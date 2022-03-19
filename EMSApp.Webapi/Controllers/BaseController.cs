using AutoMapper;
using EMSApp.Core.Interfaces;
using EMSApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace EMSApp.Webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<T> : ControllerBase
    {
        public ILazyServiceProvider LazyServiceProvider =>
            HttpContext.RequestServices.GetService<ILazyServiceProvider>();

        protected ILogger<T> Logger => LazyServiceProvider.LazyGetService<ILogger<T>>();
        protected IMapper Mapper => LazyServiceProvider.LazyGetService<IMapper>();
        protected ITenantContext TenantContext => LazyServiceProvider.LazyGetService<ICurrentTenantContextAccessor>().TenantContext;
        public BaseController(){}
    }
}
