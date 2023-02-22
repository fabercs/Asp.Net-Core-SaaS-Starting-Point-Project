namespace EMSApp.Infrastructure.MultiTenancy;

public class StrategyInfo
{
    public Type? StrategyType { get; set; }
    
    public IMultiTenantStrategy? Strategy { get; set; }
}