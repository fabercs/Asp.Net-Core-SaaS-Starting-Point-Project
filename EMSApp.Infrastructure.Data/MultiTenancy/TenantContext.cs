using EMSApp.Core.Entities;

namespace EMSApp.Infrastructure.Data.MultiTenancy
{
    public interface ITenantContext {
        Tenant Tenant { get; set; }
    }
    public class TenantContext : ITenantContext
    {
        public Tenant Tenant { get; set; }
    }
}
