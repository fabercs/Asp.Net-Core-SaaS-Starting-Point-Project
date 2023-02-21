using EMSApp.Infrastructure.MultiTenancy;

namespace EMSApp.Infrastructure.MultiTenancy;

public class StoreInfo<TTenantInfo> where TTenantInfo: ITenantInfo, new()
{
    public Type? StoreType { get; internal set; }
    
    public IMultiTenantStore<TTenantInfo>? Store { get; internal set; }
}