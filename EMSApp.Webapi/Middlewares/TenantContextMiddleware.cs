using EMSApp.Core.Interfaces;
using EMSApp.Infrastructure.Data.MultiTenancy;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EMSApp.Webapi.Middlewares
{
    public class TenantContextMiddleware
    {
        private readonly RequestDelegate _next;
        public TenantContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITenantProvider tenantProvider, 
            ICurrentTenantContextAccessor tenantContextAccessor)
        {
            var tenant = await tenantProvider.GetCurrentTenant();
            
            try
            {
                tenantContextAccessor.TenantContext = new TenantContext { Tenant = tenant };
                await _next(context);
            }
            finally
            {
                tenantContextAccessor.TenantContext = null;
            }
        }
    }
}
