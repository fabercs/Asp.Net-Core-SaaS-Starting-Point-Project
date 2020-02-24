using EMSApp.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("Licence")]
    public class Licence : BaseEntity
    {
        public LicenceType LicenceType { get; set; }
    }
}
