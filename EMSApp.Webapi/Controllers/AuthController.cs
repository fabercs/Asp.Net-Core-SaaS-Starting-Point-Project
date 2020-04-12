﻿using EMSApp.Core.DTO;
using EMSApp.Core.DTO.Requests;
using EMSApp.Core.DTO.Responses;
using EMSApp.Core.Services;
using EMSApp.Webapi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EMSApp.Webapi.Controllers
{
    public class AuthController : BaseController<AuthController>
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [Validate]
        public async Task<IActionResult> Register([FromBody]RegisterRequest registerRequest)
        {
            var response = await _authService.Register(registerRequest);

            if (!response.Success)
            {
                return BadRequest(new { errors = response.Errors });
            };
            return Ok(response.Data);
        }

        [AllowAnonymous]
        [HttpGet("verify")]
        public async Task<IActionResult> Verify([FromQuery] Guid tcid, [FromQuery]string token)
        {
            //TODO: redirection to a user-friendly error page
            if (tcid == Guid.Empty)
                return BadRequest(new Error { Description = $"{nameof(tcid)} is not valid" });

            if (string.IsNullOrWhiteSpace(token))
                return BadRequest(new Error { Description = $"{nameof(token)} is not valid" });

            var response = await _authService.Verify(tcid, token);
            if (response.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { errors = response.Errors });
            }
        }

        [AllowAnonymous]
        [TenantRequired]
        [Validate]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]LoginRequest loginRequest)
        {
            var response = await _authService.Authenticate(loginRequest);
            if (!response.Success)
            {
                return BadRequest(new { errors = response.Errors });
            };
            return Ok(response.Data);

        }

        [HttpPost("refreshtoken")]
        [Validate]
        public async Task<IActionResult> RefreshToken([FromBody]ExchangeTokenRequest tokenRequest)
        {
            Response<AuthResponse> response = await _authService.ExchangeRefreshToken(tokenRequest);
            if (!response.Success)
            {
                return BadRequest(new { errors = response.Errors });
            };
            return Ok(response.Data);
        }
    }
}