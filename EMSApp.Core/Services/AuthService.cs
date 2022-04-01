using Ardalis.GuardClauses;
using EMSApp.Core.DTO;
using EMSApp.Core.DTO.Requests;
using EMSApp.Core.DTO.Responses;
using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;
using EMSApp.Core.Validator;
using EMSApp.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Action = EMSApp.Core.Entities.Action;


namespace EMSApp.Core.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResponse>> Authenticate(LoginRequest loginRequest);
        Task<ApiResponse<RegisterResponse>> Register(RegisterRequest resgisterRequest);
        Task<ApiResponse<bool>> Verify(Guid tcid, string token);
        Task<ApiResponse<AuthResponse>> ExchangeRefreshToken(ExchangeTokenRequest tokenRequest);
    }
    public class AuthService : BaseService, IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtAuthResponseFactory _jwtTokenFactory;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IHostRepository _hostRepository;
        private readonly ITenantService _tenantService;
        private readonly IRoleService _roleService;
        private readonly IEmailService _emailService;
        private readonly IEncryptionService _encryptionService;

        public AuthService(IEmailService emailService,
            IEncryptionService encryptionService,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IRoleService roleService,
            SignInManager<ApplicationUser> signInManager,
            IJwtAuthResponseFactory jwtTokenFactory,
            TokenValidationParameters tokenValidationParameters,
            IHostRepository hostRepository,
            ITenantService tenantService, ILazyServiceProvider serviceProvider) : base(serviceProvider)
        {
            _emailService = emailService;
            _encryptionService = encryptionService;
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _roleService = roleService;
            _signInManager = signInManager;
            _jwtTokenFactory = jwtTokenFactory;
            _tokenValidationParameters = tokenValidationParameters;
            _hostRepository = hostRepository;
            _tenantService = tenantService;

        }
        public async Task<ApiResponse<RegisterResponse>> Register(RegisterRequest registerRequest)
        {
            var existingTenant = await _tenantService.GetTenantByHostname(registerRequest.Appname);
            if (existingTenant != null)
            {
                return ApiResponse<RegisterResponse>.Error(ErrorProvider.GetErrorMessage("no_tenant"));
            }
            if (registerRequest.Password != registerRequest.PasswordAgain)
            {
                return ApiResponse<RegisterResponse>.Error();
                //ErrorProvider.GetError("register_password_mismatch")
            }

            var existingUser = _hostRepository.Get<Tenant>(t => t.Responsibles.Any(r => r.Email == registerRequest.Email)
                    , includeProperties: "Responsibles").FirstOrDefault();

            if (existingUser != null)
            {
                return ApiResponse<RegisterResponse>.Error();
                //ErrorProvider.GetError("register_email_in_use")
            }

            Tenant tenant = await CreatePoteantialTenant(registerRequest);
            await ComposeEmailVerificationEmail(tenant);

            return ApiResponse<RegisterResponse>.Success();
        }
        public async Task<ApiResponse<bool>> Verify(Guid tcid, string token)
        {

            Guard.Against.NullOrWhiteSpace(token, nameof(token));
            Guard.Against.NullOrEmpty(tcid, nameof(tcid));

            var tenantContact = await _hostRepository.GetFirstAsync<TenantContact>(tc => tc.Id == tcid,
            includeProperties: "Tokens");

            if (tenantContact == null)
            {
                return ApiResponse<bool>.Error();  //ErrorProvider.GetError("no_tenant_contact")
            }

            var validToken = tenantContact.Tokens.FirstOrDefault(t => t.Value == token);

            if (validToken != null && !validToken.Valid)
            {
                return ApiResponse<bool>.Error(); //ErrorProvider.GetError("token_issued")
            }

            var validDurationPassed = validToken.CreatedOn.AddHours(24) < DateTime.Now;
            if (validDurationPassed)
            {
                return ApiResponse<bool>.Error(); // ErrorProvider.GetError("token_expired")
            }
            validToken.Valid = false;
            tenantContact.EmailConfirmed = true;
            _hostRepository.Update(tenantContact);
            await _hostRepository.SaveAsync();

            await CreateTenantResources(tenantContact.Email, tenantContact.PasswordHash);

            return ApiResponse<bool>.Success();
        }
        public async Task<ApiResponse<LoginResponse>> Authenticate(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Username);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, true, false);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var permissions = await _roleService.GetRolesPermissions(roles.ToArray(), user.TenantId);
                    var tenant = await _tenantService.GetTenantById(user.TenantId);
                    user.Tenant = tenant;

                    var authResponse = await _jwtTokenFactory.GenerateAuthResponseForUser(user);

                    var userDto = Mapper.Map<UserDto>(user);
                    userDto.PermittedPages = Mapper.Map<List<PageDto>>(permissions.Value.SelectMany(m => m.Pages));
                    userDto.Tenant = Mapper.Map<TenantDto>(user.Tenant);

                    var loginResponse = new LoginResponse
                    {
                        AccessToken = authResponse.AccessToken.Token,
                        RefreshToken = authResponse.RefreshToken,
                        User = userDto
                    };

                    return ApiResponse<LoginResponse>.Success(loginResponse);
                }
                else
                {
                    return ApiResponse<LoginResponse>.Error(); //ErrorProvider.GetError("auth_invalid_user_pass")
                }
            }
            else
            {
                return ApiResponse<LoginResponse>.Error(ErrorProvider.GetErrorMessage("auth_user_not_found"));
            }
        }
        public async Task<ApiResponse<AuthResponse>> ExchangeRefreshToken(ExchangeTokenRequest tokenRequest)
        {
            var refreshToken = _hostRepository.GetFirst<RefreshToken>(r => r.Token == tokenRequest.RefreshToken);

            ClaimsPrincipal principal = GetPrincipalFromToken(tokenRequest.AccessToken);

            if (principal == null)
            {
                return ApiResponse<AuthResponse>.Error(); //ErrorProvider.GetError("no_valid_token")
            }

            var expiryDate = long.Parse(principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeLocal = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDate).ToLocalTime();
            var jti = principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            if (expiryDateTimeLocal > DateTime.Now)
            {
                return ApiResponse<AuthResponse>.Error(); //ErrorProvider.GetError("token_not_expired") });
            }

            if (refreshToken == null)
            {
                return ApiResponse<AuthResponse>.Error(); //ErrorProvider.GetError("token_not_found") });
            }

            if (refreshToken.Invalidated)
            {
                return ApiResponse<AuthResponse>.Error(); //ErrorProvider.GetError("no_valid_token") });
            }

            if (refreshToken.ExpiresOn < DateTime.Now)
            {
                refreshToken.Invalidated = true;
                _hostRepository.Update(refreshToken);
                await _hostRepository.SaveAsync();
                return ApiResponse<AuthResponse>.Error(); //ErrorProvider.GetError("token_expired") });
            }

            if (refreshToken.JwtId != jti)
            {
                return ApiResponse<AuthResponse>.Error(); //ErrorProvider.GetError("token_mismatch") });
            }

            var user = await _userManager.FindByIdAsync(refreshToken.ApplicationUserId.ToString());
            if (user != null)
            {
                var tokens = await _jwtTokenFactory.GenerateAuthResponseForUser(user);
                refreshToken.Invalidated = true;
                _hostRepository.Update(refreshToken);
                await _hostRepository.SaveAsync();

                return ApiResponse<AuthResponse>.Success(tokens);
            }
            else
            {
                return ApiResponse<AuthResponse>.Error(); //ErrorProvider.GetError("auth_user_not_found") });
            }
        }

        private async Task<Tenant> CreatePoteantialTenant(RegisterRequest registerRequest)
        {
            var tenant = new Tenant
            {
                AppName = registerRequest.Appname.Trim(),
                Host = registerRequest.Hostname.Trim(),
                Responsibles = new List<TenantContact>
                {
                    new TenantContact {
                        Email = registerRequest.Email,
                        Name = registerRequest.Name,
                        EmailConfirmed = false,
                        PasswordHash = _encryptionService.Encrypt(registerRequest.Password),
                        Surname = registerRequest.Surname,
                        Tokens = new List<TenantContactToken>
                        {
                            new TenantContactToken
                            {
                                Name = "EmailConfirmationToken",
                                Valid = true,
                                Value = Guid.NewGuid().ToString().Replace("-",string.Empty)
                            }
                        }
                    }
                },
                TenantSetting = new TenantSetting { Language = "tr-TR" }
            };

            var tenantLicence = new TenantLicence
            {
                Tenant = tenant,
                Licence = new Licence { LicenceType = Enums.LicenceType.Trial }
            };

            tenant.Licences = new List<TenantLicence> { tenantLicence };

            //finally insert tenant to host database (and additional other info)
            _hostRepository.Create(tenant);
            await _hostRepository.SaveAsync();
            return tenant;
        }
        private async Task CreateTenantResources(string email, string password)
        {
            _roleManager.RoleValidators.Clear();
            _roleManager.RoleValidators.Add(new CustomRoleValidator());

            var tenantContact = _hostRepository.GetFirst<TenantContact>(tc => tc.Email == email && tc.PasswordHash == password,
                includeProperties: "Tenant");

            var tenant = tenantContact.Tenant;

            if (tenant.ResourcesCreated)
                return;

            string dbName = GenerateDbName(tenant.AppName);
            string connectionString = GenerateConnectionString(dbName);
            string dbScript = await GetDbScript();

            var appAdminRole = new ApplicationRole
            {
                Id = Guid.NewGuid(),
                Name = "Appadmin",
                NormalizedName = "APPADMIN",
                TenantId = tenant.Id
            };
            var roleResult = await _roleManager.CreateAsync(appAdminRole);

            if (!roleResult.Succeeded)
            {
                throw new ApplicationException(
                    string.Join(",", roleResult.Errors.Select(r => r.Description)));
            }

            var applicationUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = email,
                NormalizedEmail = email,
                NormalizedUserName = email,
                Email = email,
                EmailConfirmed = tenantContact.EmailConfirmed,
                Fullname = $"{tenantContact.Name} {tenantContact.Surname}",
                TenantId = tenant.Id,
            };

            var userResult = await _userManager.CreateAsync(applicationUser, _encryptionService.Decrypt(tenantContact.PasswordHash));

            if (userResult.Succeeded)
            {
                await AddUserToRole(applicationUser, appAdminRole);
                await CreateAdminPermissions(appAdminRole);

                tenant.ConnectionString = connectionString;
                tenant.ResourcesCreated = true;
                _hostRepository.Update(tenant);
                await _hostRepository.SaveAsync();

                await _hostRepository.ExecuteSqlCommand($"CREATE DATABASE {dbName};");

                using var connection = new NpgsqlConnection(connectionString);
                using var command = new NpgsqlCommand($"{dbScript}", connection);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                await ReloadTenantsToCache();
            }
            else
            {
                throw new ApplicationException(
                    string.Join(",", userResult.Errors.Select(r => r.Description)));
            }
        }
        private async Task CreateAdminPermissions(ApplicationRole appAdminRole)
        {
            var actions = await _hostRepository.GetAllAsync<Action>();
            foreach (var action in actions)
            {
                var appRoleAction = new ApplicationRoleAction
                {
                    ApplicationRoleId = appAdminRole.Id,
                    ActionId = action.Id
                };
                _hostRepository.Create(appRoleAction);
            }
            await _hostRepository.SaveAsync();
        }
        private async Task AddUserToRole(ApplicationUser applicationUser, ApplicationRole role)
        {
            //Workaround, TODO:implement own userrole class and own user/role store
            var userId = new NpgsqlParameter("@userId", applicationUser.Id);
            var roleId = new NpgsqlParameter("@roleId", role.Id);
            var sqlCommand = "INSERT INTO public.\"AspNetUserRoles\"(\"UserId\", \"RoleId\") VALUES(@userId, @roleId)";
            await _hostRepository.ExecuteSqlCommand(sqlCommand, userId, roleId);
        }
        private async Task ReloadTenantsToCache()
        {
            Cache.Remove("tenants");
            await Cache.GetOrCreateAsync("tenants", async t =>
            {
                t.SetSlidingExpiration(TimeSpan.FromMinutes(30));
                var list = await _hostRepository.GetAsync<Tenant>(t => t.ResourcesCreated);
                return list.ToList();
            });
        }
        /// <summary>
        /// Sample db script from local storage
        /// In real, this script will be updated from tenant app continuously
        /// and will be pushed to cloud storage as raw sql. Then, that script will
        /// be fetched here, for generating new tenant's db
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetDbScript()
        {
            //TODO: Implement cloud storage
            var rawSql = "";
            using (var sr = new StreamReader(@"D:/SqlScript/latest.sql"))
            {
                rawSql = await sr.ReadToEndAsync();
            }
            return rawSql.Replace("GO", string.Empty);
        }
        private string GenerateConnectionString(string dbName)
        {
            //TODO: parametric connectionstring template
            return $"User ID=postgres;Password=Alk11-99;Server=localhost;Port=5432;Database={dbName};Integrated Security=true;Pooling=true;";
        }
        private string GenerateDbName(string appname)
        {
            var uid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
            return $"{appname}_{uid.ToLower()}";
        }
        private async Task ComposeEmailVerificationEmail(Tenant tenant)
        {
            var tenantContact = tenant.Responsibles.FirstOrDefault();
            var emailConfirmationToken = tenantContact.Tokens.FirstOrDefault(t => t.Name == "EmailConfirmationToken");
            var basePath = _configuration.GetValue<string>("AppBasePath");
            var callbackUrl = $"{basePath}/completeregister?tcid={tenantContact.Id}&token={emailConfirmationToken.Value}";
            var message = $"Please verify your email by clicking {callbackUrl}";

            await _emailService.SendEmailAsync(tenant.Responsibles.FirstOrDefault()?.Email, "ExMS Verify your email",
                message);
        }
        private ClaimsPrincipal GetPrincipalFromToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenValidationParam = _tokenValidationParameters.Clone();
            tokenValidationParam.ValidateLifetime = false;
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParam, out var validatedToken);
            if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
            {
                return null;
            }
            return principal;

        }
        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
