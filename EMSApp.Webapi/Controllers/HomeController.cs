﻿using Microsoft.AspNetCore.Mvc;

namespace EMSApp.Webapi.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new string[] { "1", "2", "3" });
        }

    }
}