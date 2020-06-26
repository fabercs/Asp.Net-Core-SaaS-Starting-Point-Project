using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("ApplicationRoleAction")]
    public class ApplicationRoleAction : BaseEntity
    {
        public Guid ApplicationRoleId { get; set; }
        public ApplicationRole ApplicationRole { get; set; }
        public int ActionId { get; set; }
        public Action Action { get; set; }
    }
}
