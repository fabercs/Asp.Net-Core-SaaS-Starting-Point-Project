namespace EMSApp.Infrastructure.MultiTenancy;

public class StrategyInfo
{
    public Type? Info { get; set; }
    
    public IMultiTenantStrategy? Strategy { get; set; }
}