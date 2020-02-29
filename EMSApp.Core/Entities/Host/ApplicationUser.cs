using EMSApp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("ApplicationUser")]
    public class ApplicationUser : IdentityUser<Guid>, IEntity<Guid>
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? ModifedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string Fullname { get; set; }
        [Timestamp]
        public byte[] Version { get; set; }
        object IEntity.Id { get => Id; set { } }
        public Guid TenantId { get; set; }
        
        [ForeignKey(nameof(TenantId))]
        public Tenant Tenant { get; set; }
    }
}
