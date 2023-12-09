using System;
using EMSApp.Core.DTO.Requests;
using EMSApp.Core.Entities;
using EMSApp.Core.Services;
using EMSApp.Shared;
using EMSApp.Webapi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMSApp.Webapi.Controllers
{
    [Authorize]
    public class RoleController : BaseController<RoleController>
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ApplicationRole>>> GetAll()
        {
            var response = await _roleService.GetAll((Guid)TenantContext.TenantInfo.Id);
            return response.ToActionResult(this);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateRole(RoleCreateRequest request)
        {
            var response = await _roleService.CreateRole(request.RoleName, (Guid)TenantContext.TenantInfo.Id);
            return response.ToActionResult(this);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteRole(RoleDeleteRequest request)
        {
            var response = await _roleService.DeleteRole(request.RoleName);
            return response.ToActionResult(this);
        }
    }
}