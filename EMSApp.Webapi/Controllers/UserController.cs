using EMSApp.Core.DTO;
using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;
using EMSApp.Core.Services;
using EMSApp.Shared;
using EMSApp.Webapi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMSApp.Webapi.Controllers
{
    [Authorize]
    public class UserController : BaseController<UserController>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ApplicationUser>>> GetAll()
        {
            var response = await _userService.GetAllUsersOfTenant((Guid)TenantContext.TenantInfo.Id);
            return response.ToActionResult(this);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ApplicationUser>> GetById(Guid id)
        {
            var response = await _userService.GetUserById(id);
            return response.ToActionResult(this);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create(AppUserCreateRequest request)
        {
            var applicationUser = new ApplicationUser
            {
                Fullname = $"{request.Name.Trim()} {request.Surname.Trim()}",
                Email = request.Email,
                EmailConfirmed = true,
                NormalizedEmail = request.Email,
                NormalizedUserName = request.Email,
                UserName = request.Email,
                TenantId = (Guid)TenantContext.TenantInfo.Id
            };
            var response = await _userService.Create(applicationUser, request.Password);
            return response.ToActionResult(this);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(Guid userId)
        {
            var response = await _userService.Delete(userId);
            return response.ToActionResult(this);
        }
    }
}