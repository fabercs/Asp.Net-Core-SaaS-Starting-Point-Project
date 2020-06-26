using EMSApp.Core.Entities;
using System.Collections.Generic;

namespace EMSApp.Core.DTO.Responses
{

    public class AuthResponse
    {
        public AccessToken AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public ApplicationUser User { get; set; }
        public List<Module> Modules { get; set; }
    }
}
