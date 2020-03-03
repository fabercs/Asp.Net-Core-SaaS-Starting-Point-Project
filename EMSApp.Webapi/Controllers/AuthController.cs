using EMSApp.Core.DTO.Requests;
using EMSApp.Core.DTO.Responses;
using EMSApp.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EMSApp.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController<AuthController>
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage)));
            }
            if (registerRequest.Password != registerRequest.PasswordAgain)
            {
                return BadRequest("Password mismatch");
            }
            //TODO: what is after register suceeded scenario?
            var response = await _authService.Register(registerRequest);

            if (!response.Success)
            {
                return BadRequest(response.Errors);
            };
            return Ok(response.Data);
        }

        [AllowAnonymous]
        [HttpGet("verify")]
        public async Task<IActionResult> Verify([FromQuery] Guid tcid, [FromQuery]string token)
        {
            if (tcid == Guid.Empty)
                return BadRequest($"{nameof(tcid)} is not valid");

            if (string.IsNullOrWhiteSpace(token))
                return BadRequest($"{nameof(token)} is not valid");

            var response = await _authService.Verify(tcid, token);
            if (response.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage)));
            }
            var response = await _authService.Authenticate(loginRequest);
            if (!response.Success)
            {
                return BadRequest(response.Errors);
            };
            return Ok(response.Data);

        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody]ExchangeTokenRequest tokenRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage)));
            }
            Response<AuthResponse> response = await _authService.ExchangeRefreshToken(tokenRequest);
            if (!response.Success)
            {
                return BadRequest(response.Errors);
            };
            return Ok(response.Data);
        }
    }
}