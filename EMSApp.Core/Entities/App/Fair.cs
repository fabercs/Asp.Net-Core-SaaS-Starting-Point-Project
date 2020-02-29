using System;
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
        public int CountryId{ get; set; }
        public int CityId { get; set; }
        public string VenueGeoLocation { get; set; }
        public string Description { get; set; }
        public Guid? ResponsibleUserId { get; set; }
    }
}
