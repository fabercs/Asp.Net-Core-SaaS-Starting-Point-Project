using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace EMSApp.Webapi.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        [HttpGet]
        public IActionResult Get()
        {
            var tenant = TenantContext?.TenantInfo;
            return Ok();
        }

        [HttpGet("DetectLang", Name ="DetectLang")]
        public IActionResult DetectLang()
        {
            
            var acceptLang = HttpContext.Request.Headers["Accept-Language"];
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "X-Acc-Lang");
            HttpContext.Response.Headers.Add("X-Acc-Lang", acceptLang);
            return Ok(acceptLang.ToString());
        }
    }
}