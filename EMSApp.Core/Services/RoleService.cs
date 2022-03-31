using Ardalis.GuardClauses;
using EMSApp.Core.Entities;
using EMSApp.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSApp.Core.Services
{
    public interface IRoleService
    {
        Task<List<ApplicationRole>> GetAll();
        Task<Response<List<Module>>> GetRolePermissions(string id, Guid tenantId);
        Task<Response<List<Module>>> GetRolesPermissions(string[] id, Guid tenantId);
        Task<Response<bool>> CreateRole(string roleName);
        Task<Response<bool>> DeleteRole(string roleName);

    }
    public class RoleService : BaseService, IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleService(RoleManager<ApplicationRole> roleManager, ILazyServiceProvider serviceProvider) : base(serviceProvider)
        {
            _roleManager = roleManager;
        }

        public async Task<Response<bool>> CreateRole(string roleName)
        {
            Guard.Against.NullOrWhiteSpace(roleName, nameof(roleName));

            var result = await _roleManager.CreateAsync(new ApplicationRole
            {
                Name = roleName,
                NormalizedName = roleName,
                TenantId = TenantContext.Tenant.Id
            });

            var roleErrors = new List<Error>();
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    roleErrors.Add(new Error(error.Code.ToLower(), error.Description));
                }
                return Response.Fail<bool>(roleErrors);
            }
            return Response.Ok(true);
        }
        public async Task<Response<bool>> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            var result = await _roleManager.DeleteAsync(role);

            var roleErrors = new List<Error>();
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    roleErrors.Add(new Error(error.Code.ToLower(), error.Description));
                }
                return Response.Fail<bool>(roleErrors);
            }
            return Response.Ok(true);
        }
        public async Task<List<ApplicationRole>> GetAll()
         => await _roleManager.Roles.Where(r => r.TenantId == TenantContext.Tenant.Id)
            .ToListAsync();
        public async Task<Response<List<Module>>> GetRolePermissions(string id, Guid tenantId)
        {
            var modules = await AppRepository.GetAsync<Module>(
                m => m.Pages.SelectMany(p => p.Actions).SelectMany(a => a.AppRoleActions)
                .Any(ar => ar.ApplicationRole.Name == id && ar.ApplicationRole.TenantId == tenantId),
                includeProperties: "Pages.Actions.AppRoleActions.ApplicationRole");

            return Response.Ok(modules.ToList());
        }
        public async Task<Response<List<Module>>> GetRolesPermissions(string[] id, Guid tenantId)
        {
            var modules = await HostRepository.GetAsync<Module>(
                m => m.Pages.SelectMany(p => p.Actions).SelectMany(a => a.AppRoleActions)
                .Any(ar => id.Contains(ar.ApplicationRole.Name) && ar.ApplicationRole.TenantId == tenantId),
                includeProperties: "Pages.Actions.AppRoleActions.ApplicationRole");

            return Response.Ok(modules.ToList());
        }
    }
}
