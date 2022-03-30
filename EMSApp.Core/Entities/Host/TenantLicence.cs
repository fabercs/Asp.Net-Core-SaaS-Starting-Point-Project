using EMSApp.Shared;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("TenantLicence")]
    public class TenantLicence : BaseEntity
    {
        public DateTime LicenceStartDate { get; set; }
        public DateTime LicenceEndDate { get; set; }
        public bool IsActive { get; set; }

        public Guid TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public Tenant Tenant { get; set; }
        public Guid LicenceId { get; set; }

        [ForeignKey(nameof(LicenceId))]
        public Licence Licence { get; set; }
        
    }
}
