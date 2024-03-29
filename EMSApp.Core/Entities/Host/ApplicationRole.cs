﻿using EMSApp.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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
        object IEntity.Id { get; set; }
        public ICollection<ApplicationRoleAction> AppRoleActions { get; set; }
    }
}
