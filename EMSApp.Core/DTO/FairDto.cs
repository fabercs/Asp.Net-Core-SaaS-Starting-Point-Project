using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMSApp.Core.DTO
{
    public class FairCreateDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Venue { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Hall { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string CityCode { get; set; }
        public string VenueGeoLocation { get; set; }
        public string Description { get; set; }
    }

    public class FairListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Venue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Hall { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string VenueGeoLocation { get; set; }
        public string Description { get; set; }
    }

    public class FairDetailDto
    {
        public string Name { get; set; }
        public string Venue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Hall { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string VenueGeoLocation { get; set; }
        public string Description { get; set; }

        public List<FirmListDto> FirmList { get; set; } = new List<FirmListDto>();
    }
}
