using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("Fair")]
    public class Fair : BaseEntity
    {
        public string Name { get; set; }
        public string Venue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Hall { get; set; }
        public string CountryCode { get; set; }
        public string CityCode { get; set; }
        public string VenueGeoLocation { get; set; }
        public string Description { get; set; }
        public Guid? ResponsibleUserId { get; set; }

        public ICollection<Firm> Firms { get; set; } = new List<Firm>();
    }
}
