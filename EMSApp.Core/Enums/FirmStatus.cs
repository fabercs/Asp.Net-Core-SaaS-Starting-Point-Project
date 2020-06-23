using System.ComponentModel;

namespace EMSApp.Core.Enums
{
    public enum FirmStatus
    {
        [Description("Temassız")]
        Contactless=1,

        [Description("Müzakere")]
        InTalk = 2,

        [Description("Müşahede")]
        OnHold = 3,

        [Description("Pasif")]
        Passive = 4,
    }
}
