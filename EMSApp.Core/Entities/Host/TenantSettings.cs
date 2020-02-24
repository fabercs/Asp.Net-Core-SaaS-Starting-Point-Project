using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("TenantSetting")]
    public class TenantSetting : BaseEntity
    {
        public string DatetimeZone { get; set; }
        public string Language { get; set; }
        public string Currency { get; set; }
        public string FloatingPointChar { get; set; }
        public string ThousandSeperatorChar { get; set; }
    }
}
