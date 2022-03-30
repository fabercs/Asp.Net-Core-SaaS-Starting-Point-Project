using EMSApp.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("Module")]
    public class Module : BaseEntity<int>
    {
        public string Name { get; set; }
        public ICollection<Page> Pages { get; set; } = new List<Page>();
        public ICollection<LicenceModule> LicenceModules { get; set; }
    }

    [Table("Page")]
    public class Page : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string FileUrl { get; set; }
        public string Component { get; set; }
        public string Icon { get; set; }
        public string Type { get; set; }
        public int ModuleId { get; set; }
        [ForeignKey(nameof(ModuleId))]
        public Module Module { get; set; }
        public ICollection<Action> Actions { get; set; } = new List<Action>();
    }

    [Table("Action")]
    public class Action : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int PageId { get; set; }
        [ForeignKey(nameof(PageId))]
        public Page Page { get; set; }
        public ICollection<ApplicationRoleAction> AppRoleActions { get; set; }
    }

    
}
