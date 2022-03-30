using EMSApp.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("TenantContact")]
    public class TenantContact : BaseEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PasswordHash { get; set; }
        public bool EmailConfirmed { get; set; }
        public Guid TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public Tenant Tenant { get; set; }
        public ICollection<TenantContactToken> Tokens { get; set; }
    }
}
