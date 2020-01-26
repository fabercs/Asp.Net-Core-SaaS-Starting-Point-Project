using EMSApp.Infrastructure.Data.DbContextConfig;
using EMSApp.Infrastructure.Data.MultiTenancy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EMSApp.Webapi.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected EMSAppDbContext DbContext { get; set; }
        protected TenantContext CurrentTenantContext { get; set; }
        public BaseController(IServiceProvider provider)
        {
            DbContext ??= provider.GetService<EMSAppDbContext>();
            CurrentTenantContext ??= provider.GetService<ICurrentTenantContextAccessor>()?.CurrentTenant;
        }
    }
}
