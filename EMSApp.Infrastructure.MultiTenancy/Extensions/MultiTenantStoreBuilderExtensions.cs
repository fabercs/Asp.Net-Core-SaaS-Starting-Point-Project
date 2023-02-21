using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EMSApp.Infrastructure.MultiTenancy;

public static partial class MultiTenantBuilderExtensions
{
    public static MultiTenantBuilder<TTenantInfo> WithInMemoryStore<TTenantInfo>(this MultiTenantBuilder<TTenantInfo> builder,
        Action<InMemoryStoreOptions<TTenantInfo>> config)
        where TTenantInfo : class, ITenantInfo, new()
    {
        if (config == null)
        {
            throw new ArgumentNullException(nameof(config));
        }
        builder.Services.Configure(config);
        
        return builder.WithStore<InMemoryStore<TTenantInfo>>(ServiceLifetime.Singleton);
    }
    
    public static MultiTenantBuilder<TTenantInfo> WithEfCoreStore<TTenantInfo>(this MultiTenantBuilder<TTenantInfo> builder)
        where TTenantInfo : class, ITenantInfo, new()
    {
        return builder.WithStore<EfCoreStore<TTenantInfo>>(ServiceLifetime.Scoped);
    }
}