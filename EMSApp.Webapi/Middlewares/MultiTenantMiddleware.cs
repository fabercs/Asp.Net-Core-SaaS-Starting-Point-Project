using EMSApp.Infrastructure.MultiTenancy;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace EMSApp.Webapi.Middlewares
{
    public class MultiTenantMiddleware
    {
        private readonly RequestDelegate _next;

        public MultiTenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var accessor = context.RequestServices.GetRequiredService<IMultiTenantContextAccessor>();

            if (accessor.MultiTenantContext == null)
            {
                var resolver = context.RequestServices.GetRequiredService<ITenantResolver>();
                var multiTenantContext = await resolver.ResolveAsync(context);
                accessor.MultiTenantContext = multiTenantContext;
            }

            await _next(context);
        }
    }
}