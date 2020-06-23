using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("FairFirm")]
    public class FairFirm : BaseEntity
    {
        public Guid FairId { get; set; }
        public Guid FirmId { get; set; }

        public Fair Fair { get; set; }
        public Firm Firm { get; set; }
    }
}
