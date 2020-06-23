using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("City")]
    public class City
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
