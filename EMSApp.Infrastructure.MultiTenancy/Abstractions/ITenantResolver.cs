namespace EMSApp.Infrastructure.MultiTenancy;

public interface ITenantResolver
{
    Task<IMultiTenantContext?> ResolveAsync(object context);
}

public interface ITenantResolver<TTenantInfo>   
    where TTenantInfo : class, ITenantInfo, new()
{
    IEnumerable<IMultiTenantStrategy> Strategies { get; }
    
    IEnumerable<IMultiTenantStore<TTenantInfo>> Stores { get; }
    
    Task<IMultiTenantContext<TTenantInfo>?> ResolveAsync(object context);
}