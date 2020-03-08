using EMSApp.Core.Entities;

namespace EMSApp.Core.Interfaces
{
    public interface ITenantContext
    {
        Tenant Tenant { get; set; }
    }
}
