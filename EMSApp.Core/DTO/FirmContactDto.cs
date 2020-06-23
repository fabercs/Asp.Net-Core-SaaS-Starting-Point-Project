using System;
using System.ComponentModel.DataAnnotations;

namespace EMSApp.Core.DTO
{
    public class FirmContactCreateDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telephone { get; set; }
        public bool Authorized { get; set; }

    }

    public class FirmContactListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public bool Authorized { get; set; }

    }

    public class FirmContactDetailDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public bool Authorized { get; set; }
    }
}
