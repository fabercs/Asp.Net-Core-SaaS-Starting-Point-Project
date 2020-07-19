using EMSApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EMSApp.Core.Validator
{
    public class CustomRoleValidator : RoleValidator<ApplicationRole>
    {

        public override Task<IdentityResult> ValidateAsync(RoleManager<ApplicationRole> manager, ApplicationRole role)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
