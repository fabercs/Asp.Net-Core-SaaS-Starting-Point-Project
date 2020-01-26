using EMSApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EMSApp.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : BaseController
    {
        public HomeController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new string[] { "1", "2", "3" });
        }

    }
}