using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EMSApp.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EMSApp.Core.DTO
{
    public class AppUserCreateRequest : IValidatableObject
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordAgain { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errorProvider = validationContext.GetService<IErrorProvider>();
            if (Password != PasswordAgain)
            {
                yield return new ValidationResult(errorProvider.GetErrorMessage("password_mismatch"));
            }
        }
    }
}
