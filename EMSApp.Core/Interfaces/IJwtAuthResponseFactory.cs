using EMSApp.Core.DTO.Responses;
using EMSApp.Core.Entities;
using System.Threading.Tasks;

namespace EMSApp.Core.Interfaces
{
    /// <summary>
    /// Creates JWT access token and refresh token
    /// </summary>
    public interface IJwtAuthResponseFactory
    {
        Task<AuthResponse> GenerateAuthResponseForUser(ApplicationUser user);
    }
}
