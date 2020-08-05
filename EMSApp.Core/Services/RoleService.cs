using EMSApp.Core.DTO;
using EMSApp.Core.DTO.Responses;
using EMSApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSApp.Core.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<ApplicationRole>> GetAll();
        Task<Response<List<Module>>> GetRolePermissions(string id, Guid tenantId);
        Task<Response<List<Module>>> GetRolesPermissions(string[] id, Guid tenantId);
        Task<Response<bool>> CreateRole(string roleName);
        Task<Response<bool>> DeleteRole(string roleName);
        
    }
    public class RoleService : BaseService, IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleService(RoleManager<ApplicationRole> roleManager, IServiceProvider provider) : base(provider)
        {
            _roleManager = roleManager;
        }

        public async Task<Response<bool>> CreateRole(string roleName)
        {
            var response = new Response<bool>();
            var result = await _roleManager.CreateAsync(new ApplicationRole
            {
                Name = roleName,
                NormalizedName = roleName,
                TenantId = TenantContext.Tenant.Id
            });
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    response.Errors.Add(new Error { Description = error.Description });
                }
            }
            else
            {
                response.Success = true;
            }
            return response;
        }
        public async Task<Response<bool>> DeleteRole(string roleName)
        {
            var response = new Response<bool>();
            var role = await _roleManager.FindByNameAsync(roleName);
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    response.Errors.Add(new Error { Description = error.Description });
                }
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public async Task<IEnumerable<ApplicationRole>> GetAll()
         => await _roleManager.Roles.Where(r => r.TenantId == TenantContext.Tenant.Id)
            .ToListAsync();

        public async Task<Response<List<Module>>> GetRolePermissions(string id, Guid tenantId)
        {
            var response = new Response<List<Module>> { Success = true };
            var modules = await AppRepository.GetAsync<Module>(
                m => m.Pages.SelectMany(p => p.Actions).SelectMany(a => a.AppRoleActions)
                .Any(ar => ar.ApplicationRole.Name == id && ar.ApplicationRole.TenantId == tenantId)
                ,
                includeProperties: "Pages.Actions.AppRoleActions.ApplicationRole");

            response.Data = modules.ToList();
            return response;
        }

        public async Task<Response<List<Module>>> GetRolesPermissions(string[] id, Guid tenantId)
        {
            var response = new Response<List<Module>> { Success = true };
            var modules = await HostRepository.GetAsync<Module>(
                m => m.Pages.SelectMany(p => p.Actions).SelectMany(a => a.AppRoleActions)
                .Any(ar => id.Contains(ar.ApplicationRole.Name) && ar.ApplicationRole.TenantId == tenantId)
                ,
                includeProperties: "Pages.Actions.AppRoleActions.ApplicationRole");

            response.Data = modules.ToList();
            return response;
        }
    }
}
