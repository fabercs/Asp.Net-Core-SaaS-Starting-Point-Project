using System.ComponentModel;

namespace EMSApp.Core.Enums
{
    public enum FirmType
    {
        [Description("Üretici")]
        Manufacturer = 1,
        [Description("Önemli Marka")]
        Brand =2,
        [Description("Özgün Kişi")]
        Individual =3

    }
}