using EMSApp.Infrastructure.Data.MultiTenancy;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EMSApp.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class FairController : BaseController
    {
        public FairController(IServiceProvider serviceProvider) :base(serviceProvider) {}
        [HttpGet]
        public IActionResult Get()
        {
            var tenantId = CurrentTenantContext.GetCurrentTenantId();
            return Ok(new { tenant = tenantId, data = DbContext.Fairs });
        }
    }
}