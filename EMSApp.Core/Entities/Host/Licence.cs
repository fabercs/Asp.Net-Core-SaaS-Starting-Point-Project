using EMSApp.Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("Licence")]
    public class Licence : BaseEntity
    {
        public LicenceType LicenceType { get; set; }

        public ICollection<LicenceModule> LicenceModules { get; set; }
    }
}
