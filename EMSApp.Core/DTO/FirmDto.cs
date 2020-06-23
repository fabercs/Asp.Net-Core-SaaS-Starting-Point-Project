using EMSApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMSApp.Core.DTO
{
    public class FirmCreateDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string ListName { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string CityCode { get; set; }
        [Required]
        public string TaxNo { get; set; }
        [Required]
        public string TaxOffice { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public FirmType FirmType { get; set; }
        public FirmStatus FirmStatus { get; set; }

    }

    public class FirmListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ListName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string TaxNo { get; set; }
        public string TaxOffice { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string FirmType { get; set; }
        public string FirmStatus { get; set; }

    }

    public class FirmDetailDto
    {
        public string Name { get; set; }
        public string ListName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string TaxNo { get; set; }
        public string TaxOffice { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string FirmType { get; set; }
        public string FirmStatus { get; set; }

        public List<FairListDto> Fairs { get; set; } = new List<FairListDto>();
        public List<FirmContactListDto> Contacts { get; set; } = new List<FirmContactListDto>();

    }
}
