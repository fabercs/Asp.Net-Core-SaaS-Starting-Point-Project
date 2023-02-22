namespace EMSApp.Infrastructure.MultiTenancy;

public interface IMultiTenantContext
{
    ITenantInfo? TenantInfo { get; }
    
    StrategyInfo? StrategyInfo { get; }
}
public interface IMultiTenantContext<TTenantInfo> where TTenantInfo : ITenantInfo, new()
{ 
    TTenantInfo? TenantInfo { get; set; }
    
    StrategyInfo? StrategyInfo { get; set; }
    
    StoreInfo<TTenantInfo>? StoreInfo { get; set; }
}