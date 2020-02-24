using EMSApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMSApp.Core.Interfaces
{
    public interface ITenantProvider
    {
        Task<Tenant> GetCurrentTenant();

        Task<List<Tenant>> GetTenants();
    }
}
