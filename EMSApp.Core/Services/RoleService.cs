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
        Task<ApiResponse<List<ApplicationRole>>> GetAll(Guid tenantId);
        Task<ApiResponse<List<Module>>> GetRolePermissions(string id, Guid tenantId);
        Task<ApiResponse<List<Module>>> GetRolesPermissions(string[] id, Guid tenantId);
        Task<ApiResponse<bool>> CreateRole(string roleName, Guid tenantId);
        Task<ApiResponse<bool>> DeleteRole(string roleName);

    }
    public class RoleService : BaseService, IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleService(RoleManager<ApplicationRole> roleManager, ILazyServiceProvider serviceProvider) : base(serviceProvider)
        {
            _roleManager = roleManager;
        }

        public async Task<ApiResponse<bool>> CreateRole(string roleName, Guid tenantId)
        {
            Guard.Against.NullOrWhiteSpace(roleName, nameof(roleName));
            Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));

            var result = await _roleManager.CreateAsync(new ApplicationRole
            {
                Name = roleName,
                NormalizedName = roleName,
                TenantId = tenantId
            });

            if (!result.Succeeded)
            {
                var roleErrors = result.Errors.Select(e => e.Description).ToArray();
                return ApiResponse<bool>.Error(roleErrors);
            }
            return ApiResponse<bool>.Success();
        }
        public async Task<ApiResponse<bool>> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                var roleErrors = result.Errors.Select(e => e.Description).ToArray();
                return ApiResponse<bool>.Error(roleErrors);
            }
            return ApiResponse<bool>.Success();
        }
        public async Task<ApiResponse<List<ApplicationRole>>> GetAll(Guid tenantId)
        {
            var roles = await _roleManager.Roles.Where(r => r.TenantId == tenantId)
                .ToListAsync();
            return ApiResponse<List<ApplicationRole>>.Success(roles);
        }
        public async Task<ApiResponse<List<Module>>> GetRolePermissions(string id, Guid tenantId)
        {
            var modules = await AppRepository.GetAsync<Module>(
                m => m.Pages.SelectMany(p => p.Actions).SelectMany(a => a.AppRoleActions)
                .Any(ar => ar.ApplicationRole.Name == id && ar.ApplicationRole.TenantId == tenantId),
                includeProperties: "Pages.Actions.AppRoleActions.ApplicationRole");

            return ApiResponse<List<Module>>.Success(modules.ToList());
        }
        public async Task<ApiResponse<List<Module>>> GetRolesPermissions(string[] id, Guid tenantId)
        {
            var modules = await HostRepository.GetAsync<Module>(
                m => m.Pages.SelectMany(p => p.Actions).SelectMany(a => a.AppRoleActions)
                .Any(ar => id.Contains(ar.ApplicationRole.Name) && ar.ApplicationRole.TenantId == tenantId),
                includeProperties: "Pages.Actions.AppRoleActions.ApplicationRole");

            return ApiResponse<List<Module>>.Success(modules.ToList());
        }
    }
}
