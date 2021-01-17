using EMSApp.Core.Enums;
using EMSApp.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("Firm")]
    public class Firm : BaseEntity, ISoftDelete
    {
        public string Name { get; set; }
        public string ListName { get; set; }
        public string CountryCode { get; set; }
        public string CityCode { get; set; }
        public string TaxNo { get; set; }
        public string TaxOffice { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public FirmType FirmType { get; set; }
        public FirmStatus FirmStatus { get; set; }
        public ICollection<FirmContact> Contacts { get; set; } = new List<FirmContact>();
        public ICollection<Fair> Fairs { get; set; } = new List<Fair>();
        public bool IsDeleted { get; set; }
    }
}
