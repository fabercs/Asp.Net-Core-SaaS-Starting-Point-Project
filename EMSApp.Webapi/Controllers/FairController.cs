﻿using EMSApp.Core.Services;
using EMSApp.Webapi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EMSApp.Webapi.Controllers
{
    [TenantRequired]
    [Authorize]
    public class FairController : BaseController<FairController>
    {
        private readonly IFairService _fairService;
        
        public FairController(IFairService fairService)
        {
            _fairService = fairService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var user = HttpContext.User;
            //var tenant = TenantContext.Tenant;
            var fairs = await _fairService.GetAll();
            
            return Ok(fairs);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var fair = await _fairService.GetById(id);
            return Ok(fair);
        }
    }
}