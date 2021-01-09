using EMSApp.Core.DTO;
using EMSApp.Core.DTO.Requests;
using EMSApp.Core.DTO.Responses;
using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;
using EMSApp.Core.Validator;
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
        Task<Response<AuthResponse>> Authenticate(LoginRequest loginRequest);
        Task<Response<RegisterResponse>> Register(RegisterRequest resgisterRequest);
        Task<Response<bool>> Verify(Guid tcid, string token);
        Task<Response<AuthResponse>> ExchangeRefreshToken(ExchangeTokenRequest tokenRequest);
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
            ITenantService tenantService,
            IServiceProvider provider) : base(provider)
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
        public async Task<Response<RegisterResponse>> Register(RegisterRequest registerRequest)
        {
            try
            {
                var existingTenant = await _tenantService.GetTenantByHostname(registerRequest.Appname);
                if( existingTenant!= null)
                {
                    return Response.Fail<RegisterResponse>(new List<Error> { _EP.GetError("register_tenant_exist") });
                }
                if (registerRequest.Password != registerRequest.PasswordAgain)
                {
                    return Response.Fail<RegisterResponse>(new List<Error> { _EP.GetError("register_password_mismatch") });
                }

                var existingUser = _hostRepository.Get<Tenant>(t => t.Responsibles.Any(r => r.Email == registerRequest.Email)
                        , includeProperties: "Responsibles").FirstOrDefault();

                if (existingUser != null)
                {
                    return Response.Fail<RegisterResponse>(new List<Error> { _EP.GetError("register_email_in_use") });
                }

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

                await ComposeEmailVerificationEmail(tenant);

                return Response.Ok(new RegisterResponse());
            }
            catch(Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return Response.Fail<RegisterResponse>(new List<Error> { _EP.GetError("server_error") });
            }
            
        }

        public async Task<Response<bool>> Verify(Guid tcid, string token)
        {
            try
            {
                var tenantContact = await _hostRepository.GetFirstAsync<TenantContact>(tc => tc.Id == tcid,
                includeProperties: "Tokens");

                if (tenantContact == null)
                {
                    return Response.Fail<bool>(new List<Error> { _EP.GetError("no_tenant_contact") });
                }

                var validToken = tenantContact.Tokens.FirstOrDefault(t => t.Value == token);

                if (validToken != null && !validToken.Valid)
                {
                    return Response.Fail<bool>(new List<Error> { _EP.GetError("token_issued") });
                }

                var validDurationPassed = validToken.CreatedOn.AddHours(24) < DateTime.Now;
                if (validDurationPassed)
                {
                    return Response.Fail<bool>(new List<Error> { _EP.GetError("token_expired") });
                }
                validToken.Valid = false;
                tenantContact.EmailConfirmed = true;
                _hostRepository.Update(tenantContact);
                await _hostRepository.SaveAsync();

                await CreateTenantResources(tenantContact.Email, tenantContact.PasswordHash);

                return Response.Ok(true);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return Response.Fail<bool>(new List<Error> { _EP.GetError("server_error") });
            }
        }

        public async Task<Response<AuthResponse>> Authenticate(LoginRequest loginRequest)
        {
            try
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
                        authResponse.User = user;
                        authResponse.Modules = permissions.Data;
                        return Response.Ok(authResponse);
                    }
                    else
                    {
                        return Response.Fail<AuthResponse>(new List<Error> { _EP.GetError("auth_invalid_user_pass") });
                    }
                }
                else
                {
                    return Response.Fail<AuthResponse>(new List<Error> { _EP.GetError("auth_user_not_found") });
                }
            }
            catch(Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return Response.Fail<AuthResponse>(new List<Error> { _EP.GetError("server_error") });
            }
        }

        public async Task<Response<AuthResponse>> ExchangeRefreshToken(ExchangeTokenRequest tokenRequest)
        {
            try
            {
                var refreshToken = _hostRepository.GetFirst<RefreshToken>(r => r.Token == tokenRequest.RefreshToken);

                ClaimsPrincipal principal = GetPrincipalFromToken(tokenRequest.AccessToken);

                if (principal == null)
                {
                    return Response.Fail<AuthResponse>(new List<Error> { _EP.GetError("no_valid_token") });
                }

                var expiryDate = long.Parse(principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expiryDateTimeLocal = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDate).ToLocalTime();
                var jti = principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                if (expiryDateTimeLocal > DateTime.Now)
                {
                    return Response.Fail<AuthResponse>(new List<Error> { _EP.GetError("token_not_expired") });
                }

                if (refreshToken == null)
                {
                    return Response.Fail<AuthResponse>(new List<Error> { _EP.GetError("token_not_found") });
                }

                if (refreshToken.Invalidated)
                {
                    return Response.Fail<AuthResponse>(new List<Error> { _EP.GetError("no_valid_token") });
                }

                if (refreshToken.ExpiresOn < DateTime.Now)
                {
                    refreshToken.Invalidated = true;
                    _hostRepository.Update(refreshToken);
                    await _hostRepository.SaveAsync();
                    return Response.Fail<AuthResponse>(new List<Error> { _EP.GetError("token_expired") });
                }

                if (refreshToken.JwtId != jti)
                {
                    return Response.Fail<AuthResponse>(new List<Error> { _EP.GetError("token_mismatch") });
                }

                var user = await _userManager.FindByIdAsync(refreshToken.ApplicationUserId.ToString());
                if (user != null)
                {
                    var tokens = await _jwtTokenFactory.GenerateAuthResponseForUser(user);
                    refreshToken.Invalidated = true;
                    _hostRepository.Update(refreshToken);
                    await _hostRepository.SaveAsync();

                    return Response.Ok(tokens);
                }
                else
                {
                    return Response.Fail<AuthResponse>(new List<Error> { _EP.GetError("auth_user_not_found") });
                }
            }
            catch(Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return Response.Fail<AuthResponse>(new List<Error> { _EP.GetError("server_error") });
            }
        }


        #region Privates

        private async Task CreateTenantResources(string email, string password)
        {
            _roleManager.RoleValidators.Clear();
            _roleManager.RoleValidators.Add(new CustomRoleValidator());

            var tenantContact = _hostRepository.GetFirst<TenantContact>(tc => tc.Email == email && tc.PasswordHash == password,
                includeProperties: "Tenant");
            var tenant = tenantContact.Tenant;
            if (tenant.ResourcesCreated)
                return;

            string dbName = CreateDbName(tenant.AppName);
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
            try
            {
                //Workaround, TODO:implement own userrole class and own user/role store
                var userId = new NpgsqlParameter("@userId", applicationUser.Id);
                var roleId = new NpgsqlParameter("@roleId", role.Id);
                var sqlCommand = "INSERT INTO public.\"AspNetUserRoles\"(\"UserId\", \"RoleId\") VALUES(@userId, @roleId)";
                await _hostRepository.ExecuteSqlCommand(sqlCommand, userId, roleId);
            }
            catch(Exception ex)
            {
                throw ex;
            }

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
        private string CreateDbName(string appname)
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

            try
            {
                var tokenValidationParam = _tokenValidationParameters.Clone();
                tokenValidationParam.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParam, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }
                return principal;
            }
            catch(Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return null;
            }
        }
        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion Privates
    }
}
