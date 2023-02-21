using EMSApp.Core.Interfaces;
using System;

namespace EMSApp.Infrastructure.MultiTenancy
{
    public static class MultiTenantContextExtensions
    {
        public static ITenantInfo? GetTenantInfo(this IMultiTenantContext tenantContext) =>
            tenantContext?.TenantInfo;
    }
}
