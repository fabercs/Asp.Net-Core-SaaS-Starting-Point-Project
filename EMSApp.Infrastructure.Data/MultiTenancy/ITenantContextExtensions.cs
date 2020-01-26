using System;
using System.Collections.Generic;
using System.Text;

namespace EMSApp.Infrastructure.Data.MultiTenancy
{
    public static class ITenantContextExtensions
    {
        public static Guid GetCurrentTenantId(this ITenantContext tenantContext) => tenantContext.Tenant.Id;
    }
}
