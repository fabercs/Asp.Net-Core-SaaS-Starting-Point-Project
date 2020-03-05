using EMSApp.Core.DTO;
using EMSApp.Core.DTO.Requests;
using EMSApp.Core.DTO.Responses;
using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Caching.Memory;

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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtAuthResponseFactory _jwtTokenFactory;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IHostRepository _hostRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IErrorProvider _EP;
        private readonly IEmailService _emailService;
        private readonly IEncryptionService _encryptionService;

        public AuthService(IEmailService emailService,
            IEncryptionService encryptionService,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtAuthResponseFactory jwtTokenFactory,
            TokenValidationParameters tokenValidationParameters,
            IHostRepository hostRepository,
            IErrorProvider errorProvider,
            IServiceProvider provider,
            IMemoryCache memoryCache) : base(provider)
        {
            _emailService = emailService;
            _encryptionService = encryptionService;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenFactory = jwtTokenFactory;
            _tokenValidationParameters = tokenValidationParameters;
            _hostRepository = hostRepository;
            _memoryCache = memoryCache;
            _EP = errorProvider;
        }
        public async Task<Response<RegisterResponse>> Register(RegisterRequest registerRequest)
        {
            var response = new Response<RegisterResponse>();

            if(registerRequest.Password != registerRequest.PasswordAgain)
            {
                response.Errors.Add(_EP.GetError("register_password_mismatch"));
                return response;
            }

            var existingUser = _hostRepository.Get<Tenant>(t => t.Responsibles.Any(r => r.Email == registerRequest.Email)
                    , includeProperties: "Responsibles").FirstOrDefault();

            if (existingUser != null)
            {
                response.Errors.Add(_EP.GetError("register_email_in_use"));
                return response;
            }

            var tenant = new Tenant
            {
                AppName = registerRequest.Appname.Trim(),
                Host = registerRequest.Appname.Trim(),
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
            //return appropriate response
            response.Success = true;
            return response;
        }

        public async Task<Response<bool>> Verify(Guid tcid, string token)
        {
            var response = new Response<bool>();

            try
            {
                var tenantContact = await _hostRepository.GetFirstAsync<TenantContact>(tc => tc.Id == tcid,
                includeProperties: "Tokens");

                if (tenantContact == null)
                {
                    response.Errors.Add(_EP.GetError("verify_no_tenant"));
                    return response;
                }

                var validToken = tenantContact.Tokens.FirstOrDefault(t => t.Name == "EmailConfirmationToken" && t.Valid);

                if (validToken == null)
                {
                    response.Errors.Add(_EP.GetError("no_valid_token"));
                    return response;
                }

                var validDurationPassed = validToken.CreatedOn.AddHours(24) < DateTime.Now;
                if (validDurationPassed)
                {
                    response.Errors.Add(_EP.GetError("token_expired"));
                    return response;
                }
                validToken.Valid = false;
                tenantContact.EmailConfirmed = true;
                _hostRepository.Update(tenantContact);
                await _hostRepository.SaveAsync();

                await CreateTenantResources(tenantContact.Email, tenantContact.PasswordHash);

            }
            catch (Exception ex)
            {
                //TODO:log error
                response.Errors.Add(_EP.GetError("server_error"));
                return response;
            }

            response.Data = true;
            response.Success = true;
            return response;
        }

        public async Task<Response<AuthResponse>> Authenticate(LoginRequest loginRequest)
        {
            var response = new Response<AuthResponse>();

            var user = await _userManager.FindByEmailAsync(loginRequest.Username);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, true, false);
                if (result.Succeeded)
                {
                    response.Success = true;
                    var authResponse = await _jwtTokenFactory.GenerateAuthResponseForUser(user);
                    response.Data = authResponse;
                }
                else
                {
                    response.Errors.Add(_EP.GetError("auth_invalid_user_pass"));
                }
            }
            else
            {
                response.Errors.Add(_EP.GetError("auth_user_not_found"));
            }

            return response;
        }

        public async Task<Response<AuthResponse>> ExchangeRefreshToken(ExchangeTokenRequest tokenRequest)
        {
            var response = new Response<AuthResponse>();

            var refreshToken = _hostRepository.GetFirst<RefreshToken>(r => r.Token == tokenRequest.RefreshToken);

            ClaimsPrincipal principal = GetPrincipalFromToken(tokenRequest.AccessToken);

            if (principal == null)
            {
                response.Errors.Add(_EP.GetError("no_valid_token"));
            }

            var expiryDate = long.Parse(principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeLocal = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDate).ToLocalTime();
            var jti = principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            if (expiryDateTimeLocal > DateTime.Now)
            {
                response.Errors.Add(_EP.GetError("token_not_expired"));
                return response;
            }

            if (refreshToken == null)
            {
                response.Errors.Add(_EP.GetError("token_not_found"));
                return response;
            }

            if (refreshToken.Invalidated)
            {
                response.Errors.Add(_EP.GetError("no_valid_token"));
                return response;
            }

            if (refreshToken.ExpiresOn < DateTime.Now)
            {
                refreshToken.Invalidated = true;
                _hostRepository.Update(refreshToken);
                await _hostRepository.SaveAsync();
                response.Errors.Add(_EP.GetError("token_expired"));
                return response;
            }

            if (refreshToken.JwtId != jti)
            {
                response.Errors.Add(_EP.GetError("token_mismatch"));
                return response;
            }

            var user = await _userManager.FindByIdAsync(refreshToken.ApplicationUserId.ToString());
            if (user != null)
            {
                var tokens = await _jwtTokenFactory.GenerateAuthResponseForUser(user);
                response.Success = true;
                response.Data = tokens;

                refreshToken.Invalidated = true;
                _hostRepository.Update(refreshToken);
                await _hostRepository.SaveAsync();

            }
            else
            {
                response.Errors.Add(_EP.GetError("auth_user_not_found"));
                return response;
            }
            return response;
        }


        #region Helpers

        private async Task CreateTenantResources(string email, string password)
        {
            var tenantContact = _hostRepository.GetFirst<TenantContact>(tc => tc.Email == email && tc.PasswordHash == password,
                includeProperties: "Tenant");
            var tenant = tenantContact.Tenant;
            if (tenant.ResourcesCreated)
                return;

            string dbName = CreateDbName(tenant.AppName);
            string connectionString = GenerateConnectionString(dbName);
            string dbScript = await GetDbScript();

            tenant.ConnectionString = connectionString;
            tenant.ResourcesCreated = true;
            _hostRepository.Update(tenant);

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

            var result = await _userManager.CreateAsync(applicationUser, _encryptionService.Decrypt(tenantContact.PasswordHash));
            
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(applicationUser, "Appadmin");
                await _hostRepository.SaveAsync();

                await _hostRepository.ExecuteSqlCommand($"CREATE DATABASE {dbName};");

                using var connection = new NpgsqlConnection(connectionString);
                using var command = new NpgsqlCommand($"{dbScript}", connection);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                await ReloadTenantsToCahce();
            }
            else
            {
                //TODO: log fault
            }
        }

        private async Task ReloadTenantsToCahce()
        {
            _memoryCache.Remove("tenants");
            await _memoryCache.GetOrCreateAsync("tenants", async t =>
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
            var basePath = _configuration.GetValue<string>("HostBasePath");
            var callbackUrl = $"{basePath}/api/auth/verify?tcid={tenantContact.Id}&token={emailConfirmationToken.Value}";
            var message = $"Please verify your email by clicking <a href='{callbackUrl}'>here</a>";

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
            catch
            {
                return null;
            }
        }
        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion Helpers
    }
}
