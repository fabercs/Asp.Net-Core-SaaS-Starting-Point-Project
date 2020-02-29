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
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EMSApp.Core.Services
{
    public interface IAuthService
    {
        Task<Response<AuthResponse>> Authenticate(LoginRequest loginRequest);
        Task<Response<RegisterResponse>> Register(RegisterRequest resgisterRequest);
        Task<Response<bool>> Verify(Guid tcid, string token);
    }
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtAuthResponseFactory _jwtTokenFactory;
        private readonly IHostRepository _hostRepository;
        private readonly IEmailService _emailService;
        private readonly IEncryptionService _encryptionService;

        public AuthService(IEmailService emailService,
            IEncryptionService encryptionService,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtAuthResponseFactory jwtTokenFactory,
            IHostRepository hostRepository)
        {
            _emailService = emailService;
            _encryptionService = encryptionService;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenFactory = jwtTokenFactory;
            _hostRepository = hostRepository;
        }
        public async Task<Response<RegisterResponse>> Register(RegisterRequest registerRequest)
        {
            var response = new Response<RegisterResponse>();

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
            var tenantContact = await _hostRepository.GetFirstAsync<TenantContact>(tc => tc.Id == tcid,
                includeProperties: "Tokens");

            if (tenantContact == null)
            {
                response.Errors.Add(new Error { Description = "Tenant contact not found" });
                return response;
            }

            var validToken = tenantContact.Tokens.FirstOrDefault(t => t.Name == "EmailConfirmationToken" && t.Valid);

            if (validToken == null)
            {
                response.Errors.Add(new Error { Description = "No valid token found" });
                return response;
            }

            var validDurationPassed = validToken.CreatedOn.AddHours(24) < DateTime.Now;
            if (validDurationPassed)
            {
                response.Errors.Add(new Error { Description = "Token valid duration has passed" });
                return response;
            }
            validToken.Valid = false;
            tenantContact.EmailConfirmed = true;
            _hostRepository.Update(tenantContact);
            await _hostRepository.SaveAsync();

            await CreateTenantResources(tenantContact.Email, tenantContact.PasswordHash);

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
                    response.Errors.Add(new Error { Code = "auth_failure", Description = "invalid email or password" });
                }
            }
            else
            {
                response.Errors.Add(new Error { Code = "auth_failure", Description = "user not found" });
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
            }
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
        private string GetHash(string password)
        {
            using var sha256 = SHA256.Create();
            // Send a sample text to hash.  
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            // Get the hashed string.  
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
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

        #endregion Helpers
    }
}
