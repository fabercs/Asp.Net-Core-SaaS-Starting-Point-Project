using EMSApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace EMSApp.Webapi.Filters
{
    public class TenantRequired: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var tenantContext = context.HttpContext.RequestServices.GetService<ICurrentTenantContextAccessor>();
            var errorProvider = context.HttpContext.RequestServices.GetService<IErrorProvider>();

            var tenant = tenantContext?.CurrentTenant?.Tenant;
            if (tenant == null)
            {
                context.Result = new BadRequestObjectResult(new { errors =
                    errorProvider.GetError("no_tenant")
                });
            }
            base.OnActionExecuting(context);
        }
    }
}
