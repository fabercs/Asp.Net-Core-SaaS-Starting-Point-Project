 using EMSApp.Core.DTO;
using EMSApp.Core.DTO.Requests;
using EMSApp.Core.DTO.Responses;
using EMSApp.Core.Services;
using EMSApp.Shared;
using EMSApp.Webapi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<RegisterResponse>> Register([FromBody]RegisterRequest registerRequest)
        {
            var response = await _authService.Register(registerRequest);
            return response.ToActionResult(this);
        }

        [AllowAnonymous]
        [HttpGet("verify")]
        public async Task<IActionResult> Verify([FromQuery] Guid tcid, [FromQuery]string token)
        {
            var response = await _authService.Verify(tcid, token);
            if (response.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<LoginResponse>> Authenticate([FromBody]LoginRequest loginRequest)
        {
            var response = await _authService.Authenticate(loginRequest);
            return response.ToActionResult(this);
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody]ExchangeTokenRequest tokenRequest)
        {
            var response = await _authService.ExchangeRefreshToken(tokenRequest);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Errors);
            };
            return Ok(response.Value);
        }
    }
}