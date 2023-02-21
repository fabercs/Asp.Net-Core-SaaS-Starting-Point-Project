using Microsoft.Extensions.DependencyInjection;

namespace EMSApp.Infrastructure.MultiTenancy;

public class MultiTenantBuilder<TTenantInfo> where TTenantInfo : class, ITenantInfo, new()
{
    public IServiceCollection Services { get; set; }

    public MultiTenantBuilder(IServiceCollection services)
    {
        Services = services;
    }
    
    public MultiTenantBuilder<TTenantInfo> WithStore<TStore>(ServiceLifetime lifetime, params object[] parameters)
        where TStore : IMultiTenantStore<TTenantInfo>
        => WithStore<TStore>(lifetime, sp => ActivatorUtilities.CreateInstance<TStore>(sp, parameters));
    public MultiTenantBuilder<TTenantInfo> WithStore<TStore>(ServiceLifetime lifetime,
        Func<IServiceProvider, TStore> factory)
        where TStore : IMultiTenantStore<TTenantInfo>
    {
        if (factory == null)
            throw new ArgumentNullException(nameof(factory));
        
        Services.Add(
            ServiceDescriptor.Describe(typeof(IMultiTenantStore<TTenantInfo>), sp =>
                factory(sp), lifetime)
            );
        return this;
    }
    
    public MultiTenantBuilder<TTenantInfo> WithStrategy<TStrategy>(ServiceLifetime lifetime, params object[] parameters)
        where TStrategy : IMultiTenantStrategy
        => WithStrategy<TStrategy>(lifetime, sp => ActivatorUtilities.CreateInstance<TStrategy>(sp, parameters));
    
    public MultiTenantBuilder<TTenantInfo> WithStrategy<TStrategy>(ServiceLifetime lifetime,
        Func<IServiceProvider, TStrategy> factory)
        where TStrategy : IMultiTenantStrategy
    {
        if (factory == null)
            throw new ArgumentNullException(nameof(factory));
        
        Services.Add(
            ServiceDescriptor.Describe(typeof(IMultiTenantStrategy), sp =>
                factory(sp), lifetime)
        );
        return this;
    }
}