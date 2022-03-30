using EMSApp.Shared;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("TenantContactToken")]
    public class TenantContactToken : BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Valid { get; set; }

        public Guid TenantContactId { get; set; }
        [ForeignKey(nameof(TenantContactId))]
        public TenantContact TenantContact{ get; set; }
    }
}
