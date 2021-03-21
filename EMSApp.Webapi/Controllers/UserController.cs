using EMSApp.Core.DTO;
using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;
using EMSApp.Core.Services;
using EMSApp.Webapi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EMSApp.Webapi.Controllers
{
    [TenantRequired]
    [Authorize]
    public class UserController : BaseController<UserController>
    {
        private readonly IUserService _userService;
        private readonly IErrorProvider _EP;

        public UserController(IUserService userService, IErrorProvider errorProvider)
        {
            _userService = userService;
            _EP = errorProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersOfTenant(TenantContext.Tenant.Id);
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }

        [HttpPost]
        [Validate]
        public async Task<IActionResult> Create(AppUserCreateRequest request)
        {
            if (request.Password != request.PasswordAgain)
            {
                return BadRequest(_EP.GetError("password_mismatch"));
            }
            var applicationUser = new ApplicationUser
            {
                Fullname = $"{request.Name.Trim()} {request.Surname.Trim()}",
                Email = request.Email,
                EmailConfirmed = true,
                NormalizedEmail = request.Email,
                NormalizedUserName = request.Email,
                UserName = request.Email,
                TenantId = TenantContext.Tenant.Id
            };
            var response = await _userService.Create(applicationUser, request.Password);
            if (response.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var response = await _userService.Delete(userId);
            if (response.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }
    }
}