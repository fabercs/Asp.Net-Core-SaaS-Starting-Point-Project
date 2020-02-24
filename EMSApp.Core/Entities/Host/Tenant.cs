using EMSApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    /// <summary>
    /// Tenants
    /// </summary>
    [Table("Tenant")]
    public class Tenant : BaseEntity, ISoftDelete
    {
        public string AppName { get; set; }
        public string Host { get; set; }
        public string ConnectionString { get; set; }
        public bool IsDeleted { get; set; }

        public bool ResourcesCreated { get; set; }

        public Guid TenantSettingId { get; set; }
        [ForeignKey(nameof(TenantSettingId))]
        public TenantSetting TenantSetting { get; set; }

        public ICollection<TenantLicence> Licences { get; set; }
        public ICollection<TenantContact> Responsibles { get; set; }
    }
}
