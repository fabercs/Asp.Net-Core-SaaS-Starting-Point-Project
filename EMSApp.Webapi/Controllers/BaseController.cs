using AutoMapper;
using EMSApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EMSApp.Webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<T> : ControllerBase
    {
        private ITenantContext _tenantContext { get; set; }
        private ILogger<T> _logger;
        private IMapper _mapper;
        protected ILogger<T> Logger => _logger ?? (_logger = HttpContext.RequestServices.GetService<ILogger<T>>());
        protected IMapper Mapper => _mapper ?? (_mapper = HttpContext.RequestServices.GetService<IMapper>());
        protected ITenantContext TenantContext => _tenantContext ?? 
            (_tenantContext = HttpContext.RequestServices.GetService<ICurrentTenantContextAccessor>().TenantContext);

        public BaseController(){}
    }
}
