using EMSApp.Core.DTO;
using EMSApp.Core.DTO.Responses;
using EMSApp.Core.Entities;
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
        Task<Response<IEnumerable<ApplicationUser>>> GetAllUsersOfTenant(Guid tenantId);
        Task<Response<bool>> Create(ApplicationUser user, string password);
        Task<Response<bool>> Delete(ApplicationUser user);
        Task<Response<bool>> Delete(Guid userId);
        Task<Response<bool>> Update(ApplicationUser user);
        Task<Response<bool>> AddToRole(ApplicationUser user, string role);
    }
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(
            UserManager<ApplicationUser> userManager,
            IServiceProvider provider) : base(provider)
        {
            _userManager = userManager;
        }

        public async Task<Response<bool>> AddToRole(ApplicationUser user, string role)
        {
            var response = new Response<bool>();
            var result = await _userManager.AddToRoleAsync(user, role);
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

        public async Task<Response<bool>> Create(ApplicationUser user, string password)
        {
            var response = new Response<bool>();
            var result = await _userManager.CreateAsync(user, password);
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

        public async Task<Response<bool>> Delete(ApplicationUser user)
        {
            var response = new Response<bool>();
            var result = await _userManager.DeleteAsync(user);
            
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

        public async Task<Response<bool>> Delete(Guid userId)
        {
            var response = new Response<bool>();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.DeleteAsync(user);

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

        public async Task<Response<IEnumerable<ApplicationUser>>> GetAllUsersOfTenant(Guid tenantId)
        {
            var response = new Response<IEnumerable<ApplicationUser>>();
            var users = await _userManager.Users.Where(u => u.TenantId == tenantId).ToListAsync();
            response.Data = users;
            response.Success = true;
            return response;
        }

        public async Task<Response<ApplicationUser>> GetUserById(Guid id)
        {
            var response = new Response<ApplicationUser>();
            var user = await _userManager.FindByIdAsync(id.ToString());
            response.Success = true;
            response.Data = user;
            return response;
        }

        public async Task<Response<bool>> Update(ApplicationUser user)
        {
            var response = new Response<bool>();
            var result = await _userManager.UpdateAsync(user);
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
    }
}
