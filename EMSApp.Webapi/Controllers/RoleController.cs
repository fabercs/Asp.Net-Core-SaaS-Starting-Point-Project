using EMSApp.Core.DTO.Requests;
using EMSApp.Core.Services;
using EMSApp.Shared;
using EMSApp.Webapi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EMSApp.Webapi.Controllers
{
    [TenantRequired]
    [Authorize]
    public class RoleController : BaseController<RoleController>
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAll();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleCreateRequest request)
        {
            await _roleService.CreateRole(request.RoleName);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole(RoleDeleteRequest request)
        {
            await _roleService.DeleteRole(request.RoleName);
            return Ok();
        }
    }
}