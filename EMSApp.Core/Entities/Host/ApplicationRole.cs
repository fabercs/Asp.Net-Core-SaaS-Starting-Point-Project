using EMSApp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    public class ApplicationRole : IdentityRole<Guid>, IEntity<Guid>
    {
        public Guid TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public Tenant Tenant { get; set; }
        public DateTime CreatedOn { get ; set; }
        public DateTime? ModifedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public byte[] Version { get; set; }
        object IEntity.Id { get; set; }
    }
}
