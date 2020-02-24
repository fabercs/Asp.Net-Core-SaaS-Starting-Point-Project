using EMSApp.Core.DTO;
using EMSApp.Core.DTO.Requests;
using EMSApp.Core.DTO.Responses;
using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EMSApp.Core.Services
{
    public interface IAuthService
    {
        Task<Response<RegisterResponse>> Register(RegisterRequest resgisterRequest);

        Task<Response<bool>> Verify(Guid tcid, string token);
    }
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostRepository _hostRepository;
        private readonly IEmailService _emailService;
        public AuthService(IEmailService emailService,
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            IHostRepository hostRepository)
        {
            _emailService = emailService;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _hostRepository = hostRepository;
        }
        public async Task<Response<RegisterResponse>> Register(RegisterRequest registerRequest)
        {
            var response = new Response<RegisterResponse>();

            var tenant = new Tenant
            {
                AppName = registerRequest.Appname.Trim(),
                Responsibles = new List<TenantContact>
                {
                    new TenantContact {
                        Email = registerRequest.Email,
                        Name = registerRequest.Name,
                        EmailConfirmed = false,
                        PasswordHash = GetHash(registerRequest.Password),
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

            response.Data = true;
            response.Success = true;
            return response;

        }

        #region Helpers
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
            var callbackUrl = $"{basePath}/account/verify/tcid={tenantContact.Id}&token={emailConfirmationToken.Value}";
            var message = $"Please verify your email by clicking <a href='{callbackUrl}'>here</a>";

            await _emailService.SendEmailAsync(tenant.Responsibles.FirstOrDefault()?.Email, "ExMS Verify your email",
                message);
        }
        #endregion Helpers
    }
}
