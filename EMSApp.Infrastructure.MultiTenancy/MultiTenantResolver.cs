namespace EMSApp.Infrastructure.MultiTenancy;

public class MultiTenantResolver<TTenantInfo> : ITenantResolver<TTenantInfo>, ITenantResolver
    where TTenantInfo : class, ITenantInfo, new()
{
    public IEnumerable<IMultiTenantStrategy> Strategies { get; }
    
    public IEnumerable<IMultiTenantStore<TTenantInfo>> Stores { get; }

    public MultiTenantResolver(IEnumerable<IMultiTenantStrategy> strategies, IEnumerable<IMultiTenantStore<TTenantInfo>> stores)
    {
        Strategies = strategies;
        Stores = stores;

        Strategies = Strategies.OrderBy(s => s.Priority);
    }
    public async Task<IMultiTenantContext<TTenantInfo>?> ResolveAsync(object context)
    {
        IMultiTenantContext<TTenantInfo>? result = null;

        foreach (var strategy in Strategies)
        {
            var identifier = await strategy.GetIdentifierAsync(context);
            if (identifier != null)
            {
                foreach (var store in Stores)
                {
                    result = new MultiTenantContext<TTenantInfo>();
                    var tenantInfo = await store.GetByIdentifierAsync(identifier);
                    result.StoreInfo = new StoreInfo<TTenantInfo> { Store = store, StoreType = store.GetType() };
                    result.StrategyInfo = new StrategyInfo { Strategy = strategy, StrategyType = strategy.GetType() };
                    result.TenantInfo = tenantInfo;
                    
                    break;
                }

                if (result != null)
                {
                    break;
                }
            }
        }

        return result;

    }

    async Task<IMultiTenantContext?> ITenantResolver.ResolveAsync(object context)
    {
        return (await ResolveAsync(context)) as IMultiTenantContext;
    }
}