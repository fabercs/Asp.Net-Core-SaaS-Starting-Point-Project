using EMSApp.Core.Interfaces;
using System;

namespace EMSApp.Infrastructure.Data.MultiTenancy
{
    public static class ITenantContextExtensions
    {
        public static Guid GetCurrentTenantId(this ITenantContext tenantContext) => tenantContext.Tenant.Id;
    }
}
