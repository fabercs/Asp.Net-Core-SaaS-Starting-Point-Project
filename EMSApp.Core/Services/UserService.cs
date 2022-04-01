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
    public interface IUserService
    {
        Task<ApiResponse<ApplicationUser>> GetUserById(Guid id);
        Task<ApiResponse<List<ApplicationUser>>> GetAllUsersOfTenant(Guid tenantId);
        Task<ApiResponse<bool>> Create(ApplicationUser user, string password);
        Task<ApiResponse<bool>> Delete(ApplicationUser user);
        Task<ApiResponse<bool>> Delete(Guid userId);
        Task<ApiResponse<bool>> Update(ApplicationUser user);
        Task<ApiResponse<bool>> AddToRole(ApplicationUser user, string role);
        Task<ApiResponse<bool>> RemoveFromRole(ApplicationUser user, string role);
    }
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager, ILazyServiceProvider serviceProvider) 
            : base(serviceProvider)
        {
            _userManager = userManager;
        }

        public async Task<ApiResponse<bool>> AddToRole(ApplicationUser user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            var errors = Array.Empty<string>();
            if (!result.Succeeded)
            {
                errors = result.Errors.Select(e => e.Description).ToArray();
                return ApiResponse<bool>.Error(errors);
            }
            return ApiResponse<bool>.Success();
        }

        public async Task<ApiResponse<bool>> Create(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            var errors = Array.Empty<string>();
            if (!result.Succeeded)
            {
                errors = result.Errors.Select(e => e.Description).ToArray();
                return ApiResponse<bool>.Error(errors);
            }
            return ApiResponse<bool>.Success();
        }

        public async Task<ApiResponse<bool>> Delete(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            var errors = Array.Empty<string>();
            if (!result.Succeeded)
            {
                errors = result.Errors.Select(e => e.Description).ToArray();
                return ApiResponse<bool>.Error(errors);
            }
            return ApiResponse<bool>.Success();
        }

        public async Task<ApiResponse<bool>> Delete(Guid userId)
        {

            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.DeleteAsync(user);
            var errors = Array.Empty<string>();
            if (!result.Succeeded)
            {
                errors = result.Errors.Select(e => e.Description).ToArray();
                return ApiResponse<bool>.Error(errors);
            }
            return ApiResponse<bool>.Success();
        }

        public async Task<ApiResponse<List<ApplicationUser>>> GetAllUsersOfTenant(Guid tenantId)
        {
            var users = await _userManager.Users.Where(u => u.TenantId == tenantId).ToListAsync();
            return ApiResponse<List<ApplicationUser>>.Success(users);
        }

        public async Task<ApiResponse<ApplicationUser>> GetUserById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return ApiResponse<ApplicationUser>.Success(user);
        }

        public async Task<ApiResponse<bool>> RemoveFromRole(ApplicationUser user, string role)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, role);
            var errors = Array.Empty<string>();
            if (!result.Succeeded)
            {
                errors = result.Errors.Select(e => e.Description).ToArray();
                return ApiResponse<bool>.Error(errors);
            }
            return ApiResponse<bool>.Success();
        }

        public async Task<ApiResponse<bool>> Update(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            var errors = Array.Empty<string>();
            if (!result.Succeeded)
            {
                errors = result.Errors.Select(e => e.Description).ToArray();
                return ApiResponse<bool>.Error(errors);
            }
            return ApiResponse<bool>.Success();
        }
    }
}
