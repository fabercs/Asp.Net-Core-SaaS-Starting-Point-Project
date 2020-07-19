using System.ComponentModel.DataAnnotations;

namespace EMSApp.Core.DTO.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string Hostname{ get; set; }

        [Required]
        public string Appname { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [StringLength(maximumLength:30, MinimumLength =8)]
        [Required]
        public string Password { get; set; }

        [StringLength(maximumLength: 30, MinimumLength = 8)]
        [Required]
        public string PasswordAgain { get; set; }

    }
}
