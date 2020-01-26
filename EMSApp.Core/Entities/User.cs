using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
