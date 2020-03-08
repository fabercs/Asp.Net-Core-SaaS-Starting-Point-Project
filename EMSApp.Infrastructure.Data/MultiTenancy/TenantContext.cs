using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;

namespace EMSApp.Infrastructure.Data.MultiTenancy
{
    
    public class TenantContext : ITenantContext
    {
        public Tenant Tenant { get; set; }
    }
}
