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

        public async Task InvokeAsync(HttpContext context, ITenantProvider provider, 
            ICurrentTenantContextAccessor tenantContextAccessor)
        {
            var tenant = await provider.GetCurrentTenant();
            
            try
            {
                tenantContextAccessor.CurrentTenant = new TenantContext { Tenant = tenant };
                await _next(context);
            }
            finally
            {
                tenantContextAccessor.CurrentTenant = null;
            }
        }
    }
}
