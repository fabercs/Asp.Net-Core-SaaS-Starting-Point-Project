using AutoMapper;
using EMSApp.Core.Interfaces;
using EMSApp.Infrastructure.MultiTenancy;
using EMSApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EMSApp.Webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<T> : ControllerBase
    {
        private ILazyServiceProvider LazyServiceProvider =>
            HttpContext.RequestServices.GetService<ILazyServiceProvider>();
        protected ILogger<T> Logger => LazyServiceProvider.LazyGetService<ILogger<T>>();
        protected IMapper Mapper => LazyServiceProvider.LazyGetService<IMapper>();
        protected IMultiTenantContext<TenantInfo> TenantContext => 
            LazyServiceProvider.LazyGetService<IMultiTenantContext<TenantInfo>>();
        public BaseController(){}
    }
}
