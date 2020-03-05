using AutoMapper;
using EMSApp.Infrastructure.Data.MultiTenancy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EMSApp.Webapi.Controllers
{
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        private TenantContext _tenantContext { get; set; }
        private ILogger<T> _logger;
        private IMapper _mapper;
        protected ILogger<T> Logger => _logger ?? (_logger = HttpContext.RequestServices.GetService<ILogger<T>>());
        protected IMapper Mapper => _mapper ?? (_mapper = HttpContext.RequestServices.GetService<IMapper>());
        protected TenantContext TenantContext => _tenantContext ?? 
            (_tenantContext = HttpContext.RequestServices.GetService<ICurrentTenantContextAccessor>().CurrentTenant);

        public BaseController(){}
    }
}
