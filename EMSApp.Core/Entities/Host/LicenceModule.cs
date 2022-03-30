using EMSApp.Shared;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("LicenceModule")]
    public class LicenceModule : BaseEntity
    {
        public Guid LicenceId { get; set; }
        public int ModuleId { get; set; }

        public Licence Licence { get; set; }
        public Module Module { get; set; }
    }
}
