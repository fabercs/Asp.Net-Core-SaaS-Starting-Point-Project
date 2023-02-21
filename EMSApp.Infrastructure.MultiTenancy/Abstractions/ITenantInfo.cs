namespace EMSApp.Infrastructure.MultiTenancy;

public interface ITenantInfo
{
    Guid? Id { get; set; }
    string? Identifier { get; set; }
    string? ConnectionString { get; set; }
    string? Name { get; set; }
}