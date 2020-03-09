using System.ComponentModel.DataAnnotations;

namespace EMSApp.Core.DTO
{
    public class AppUserCreateRequest
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

    }
}
