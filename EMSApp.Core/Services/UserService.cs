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
        Task<Response<ApplicationUser>> GetUserById(Guid id);
        Task<Response<List<ApplicationUser>>> GetAllUsersOfTenant(Guid tenantId);
        Task<Response<bool>> Create(ApplicationUser user, string password);
        Task<Response<bool>> Delete(ApplicationUser user);
        Task<Response<bool>> Delete(Guid userId);
        Task<Response<bool>> Update(ApplicationUser user);
        Task<Response<bool>> AddToRole(ApplicationUser user, string role);
        Task<Response<bool>> RemoveFromRole(ApplicationUser user, string role);
    }
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager, ILazyServiceProvider serviceProvider) : base(serviceProvider)
        {
            _userManager = userManager;
        }

        public async Task<Response<bool>> AddToRole(ApplicationUser user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            var userManagerErrors = new List<Error>();
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    userManagerErrors.Add(new Error(error.Code.ToLower(), error.Description));
                }
                return Response.Fail<bool>(userManagerErrors);
            }
            return Response.Ok(true);
        }

        public async Task<Response<bool>> Create(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            var userManagerErrors = new List<Error>();

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    userManagerErrors.Add(new Error(error.Code.ToLower(), error.Description));
                }
                return Response.Fail<bool>(userManagerErrors);
            }
            return Response.Ok(true);
        }

        public async Task<Response<bool>> Delete(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            var userManagerErrors = new List<Error>();

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    userManagerErrors.Add(new Error(error.Code.ToLower(), error.Description));
                }
                return Response.Fail<bool>(userManagerErrors);
            }
            return Response.Ok(true);
        }

        public async Task<Response<bool>> Delete(Guid userId)
        {

            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.DeleteAsync(user);
            var userManagerErrors = new List<Error>();

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    userManagerErrors.Add(new Error(error.Code.ToLower(), error.Description));
                }
                return Response.Fail<bool>(userManagerErrors);
            }
            return Response.Ok(true);
        }

        public async Task<Response<List<ApplicationUser>>> GetAllUsersOfTenant(Guid tenantId)
        {
            var users = await _userManager.Users.Where(u => u.TenantId == tenantId).ToListAsync();
            return Response.Ok(users);
        }

        public async Task<Response<ApplicationUser>> GetUserById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return Response.Ok(user);
        }

        public async Task<Response<bool>> RemoveFromRole(ApplicationUser user, string role)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, role);
            var userManagerErrors = new List<Error>();

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    userManagerErrors.Add(new Error(error.Code.ToLower(), error.Description));
                }
                return Response.Fail<bool>(userManagerErrors);
            }
            return Response.Ok(true);
        }

        public async Task<Response<bool>> Update(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            var userManagerErrors = new List<Error>();

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    userManagerErrors.Add(new Error(error.Code.ToLower(), error.Description));
                }
                return Response.Fail<bool>(userManagerErrors);
            }
            return Response.Ok(true);
        }
    }
}
