using EMSApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using EMSApp.Infrastructure.MultiTenancy;

namespace EMSApp.Webapi.Filters
{
    public class TenantRequired: Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var tenantContext = context.HttpContext.RequestServices.GetService<IMultiTenantContext<TenantInfo>>();
            var errorProvider = context.HttpContext.RequestServices.GetService<IErrorProvider>();

            var tenantInfo = tenantContext?.TenantInfo;
            if (tenantInfo == null)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    errors = errorProvider.GetError("no_tenant")
                });
            }
            await next();
        }
    }
}
