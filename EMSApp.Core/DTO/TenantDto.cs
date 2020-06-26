using System;

namespace EMSApp.Core.DTO
{
    public class TenantDto
    {
        public Guid Id { get; set; }
        public string AppName { get; set; }
        public string Language { get; set; }
        public string DateTimeZone { get; set; }
    }
}
