namespace EMSApp.Infrastructure.MultiTenancy;

public class InMemoryStoreOptions<TTenantInfo> where TTenantInfo : class, ITenantInfo, new()
{
    public IList<TTenantInfo> Tenants { get; set; }
}