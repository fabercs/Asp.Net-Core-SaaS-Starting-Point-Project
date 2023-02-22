using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;
using EMSApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSApp.Core.Services
{
    public interface ITenantService
    {
        Task<Tenant> GetTenantById(Guid id);
        Task<Tenant> GetTenantByHostname(string hostname);
        Task<List<Tenant>> GetAllTenants();
    }
    public class TenantService : BaseService, ITenantService
    {
        private readonly IHostRepository _hostRepository;

        public TenantService(IHostRepository hostRepository, ILazyServiceProvider serviceProvider) 
            : base(serviceProvider)
        {
            _hostRepository = hostRepository;
        }
        public async Task<Tenant> GetTenantByHostname(string hostname)
            => await _hostRepository.GetFirstAsync<Tenant>(t => t.Host == hostname, 
                includeProperties: "TenantSetting,Responsibles.Tokens");

        public async Task<List<Tenant>> GetAllTenants()
        {
            var tenants = await _hostRepository.GetAllAsync<Tenant>();
            return tenants.ToList();
        }

        public async Task<Tenant> GetTenantById(Guid id)
            => await _hostRepository.GetByIdAsync<Tenant>(id);
    }
}
