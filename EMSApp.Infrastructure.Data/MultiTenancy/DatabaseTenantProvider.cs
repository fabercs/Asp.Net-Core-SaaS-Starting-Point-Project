using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSApp.Infrastructure.Data.MultiTenancy
{
    public class DatabaseTenantProvider : ITenantProvider
    {
        private static List<Tenant> _tenants = new List<Tenant>();

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostRepository _hostRepository;
        private readonly IMemoryCache _memoryCahce;

        public DatabaseTenantProvider (IHostRepository hostRepository,
            IMemoryCache memoryCache,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostRepository = hostRepository;
            _memoryCahce = memoryCache;
        }

        private async Task LoadTenantsFromDatabase()
        {
            var tenants = await _memoryCahce.GetOrCreateAsync("tenants", async t =>
            {
                t.SetSlidingExpiration(TimeSpan.FromMinutes(30));
                var tenantList = await _hostRepository.GetAsync<Tenant>(t => t.ResourcesCreated, 
                    includeProperties:"TenantSetting");
                return tenantList.ToList();
            });
            
            _tenants = tenants.ToList();
        }

        public async Task<Tenant> GetCurrentTenant()
        {
            var tenantId = _httpContextAccessor.HttpContext.Request.Headers["X-TenantId"].ToString();

            if (string.IsNullOrWhiteSpace(tenantId))
                return null;

            await LoadTenantsFromDatabase();
            return _tenants.FirstOrDefault(t => t.Id == Guid.Parse(tenantId));
        }

        public async Task<List<Tenant>> GetTenants()
        {
            await LoadTenantsFromDatabase();
            return _tenants;
        } 
    }
}
