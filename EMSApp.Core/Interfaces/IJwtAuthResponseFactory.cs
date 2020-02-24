using EMSApp.Core.DTO.Responses;
using EMSApp.Core.Entities;
using System.Threading.Tasks;

namespace EMSApp.Core.Interfaces
{
    public interface IJwtAuthResponseFactory
    {
        Task<AuthResponse> GenerateAuthResponseForUser(ApplicationUser user);
    }
}
