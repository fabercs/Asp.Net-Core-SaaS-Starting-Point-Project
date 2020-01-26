using EMSApp.Core.Entities;
using System.Collections.Generic;

namespace EMSApp.Core.Interfaces
{
    public interface ITenantProvider
    {
        Tenant GetCurrentTenant();

        List<Tenant> GetTenants();
    }
}
