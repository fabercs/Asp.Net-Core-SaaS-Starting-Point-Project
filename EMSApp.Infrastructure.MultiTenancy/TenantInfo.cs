namespace EMSApp.Infrastructure.MultiTenancy;

public class TenantInfo : ITenantInfo
{
    public string Language { get; set; }
    
    public string DateTimeZone { get; set; }
    public Guid? Id { get; set; }
    public string? Identifier { get; set; }
    
    public string? ConnectionString { get; set; }
    
    public string? Name { get; set; }
}