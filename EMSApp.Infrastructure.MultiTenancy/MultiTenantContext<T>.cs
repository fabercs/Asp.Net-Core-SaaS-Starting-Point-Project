namespace EMSApp.Infrastructure.MultiTenancy;

public class MultiTenantContext<TTenantInfo> : IMultiTenantContext, IMultiTenantContext<TTenantInfo>
    where TTenantInfo : class, ITenantInfo, new()
{
    ITenantInfo? IMultiTenantContext.TenantInfo => TenantInfo;
    public TTenantInfo? TenantInfo { get; set; }
    public StrategyInfo? StrategyInfo { get; set; }
    public StoreInfo<TTenantInfo>? StoreInfo { get; set; }
}