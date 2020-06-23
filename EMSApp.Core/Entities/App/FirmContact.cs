using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("FirmContact")]
    public class FirmContact : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public bool Authorized { get; set; }

        public Guid FirmId { get; set; }
        public Firm Firm { get; set; }


    }
}
