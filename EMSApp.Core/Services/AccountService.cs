using EMSApp.Core.DTO;
using EMSApp.Core.DTO.Responses;
using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EMSApp.Core.Services
{
    public interface IAccountService
    {
        Task<Response<AuthResponse>> Authenticate(string username, string password);
    }
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtAuthResponseFactory _jwtTokenFactory;
        private readonly IHostRepository _hostRepository;
        private readonly IServiceProvider _serviceProvider;

        public AccountService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IJwtAuthResponseFactory jwtTokenFactory,
            IHostRepository hostRepository,
            IServiceProvider serviceProvider)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtTokenFactory = jwtTokenFactory;
            _hostRepository = hostRepository;
            _serviceProvider = serviceProvider;
        }
        public async Task<Response<AuthResponse>> Authenticate(string email, string password)
        {
            var response = new Response<AuthResponse>();

            await CreateTenantResources(email, password);

            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, true, false);
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

            response.Errors.Add(new Error { Code = "auth_failure", Description = "user not found" });

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

            await _hostRepository.ExecuteSqlCommand($"USE master CREATE DATABASE {dbName}");
            await _hostRepository.ExecuteSqlCommand($"USE {dbName} {dbScript}");

            tenant.ConnectionString = connectionString;
            _hostRepository.Update(tenant);
            await _hostRepository.SaveAsync();

            var appRepository = _serviceProvider.GetService<IAppRepository>();

            var applicationUser = new ApplicationUser
            {
                Email = email,
                EmailConfirmed = tenantContact.EmailConfirmed,
                PasswordHash = password,
                Fullname = $"{tenantContact.Name} {tenantContact.Surname}"
            };
            appRepository.Create(applicationUser);
            await appRepository.SaveAsync();
            await _roleManager.CreateAsync(new IdentityRole<Guid> { Name = "Appadmin" });
            await _userManager.AddToRoleAsync(applicationUser, "Appadmin");
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
            return $"Data Source=.\\SQLEXPRESS;Initial Catalog={dbName};Integrated Security=True;";
        }
        private string CreateDbName(string appname)
        {
            var uid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
            return $"{ appname}_{uid}";
        }

        #endregion Helpers


    }
}
