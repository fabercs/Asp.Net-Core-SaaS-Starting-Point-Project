using EMSApp.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EMSApp.Webapi.Middlewares
{
    public class MissingTenantMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _missingTenantUrl;

        public MissingTenantMiddleware(RequestDelegate next, string missingTenantUrl)
        {
            _missingTenantUrl = missingTenantUrl;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ITenantProvider tenantProvider)
        {
            if(tenantProvider.GetCurrentTenant() == null)
            {
                
            }
            await _next(context);

        }
    }
}
