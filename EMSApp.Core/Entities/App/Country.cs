using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("Country")]
    public class Country
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
