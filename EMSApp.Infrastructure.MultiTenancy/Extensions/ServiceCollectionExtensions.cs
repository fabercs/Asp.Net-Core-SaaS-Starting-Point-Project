using Microsoft.Extensions.DependencyInjection;

namespace EMSApp.Infrastructure.MultiTenancy;

public static class ServiceCollectionExtensions
{
    public static MultiTenantBuilder<TTenantInfo> AddMultiTenancy<TTenantInfo>(this IServiceCollection services)
        where TTenantInfo : class, ITenantInfo, new()
    {
        services.AddScoped<ITenantResolver<TTenantInfo>, MultiTenantResolver<TTenantInfo>>();
        services.AddScoped<ITenantResolver>(sp => 
            (ITenantResolver)sp.GetRequiredService<ITenantResolver<TTenantInfo>>());

        services.AddScoped<IMultiTenantContext<TTenantInfo>>(sp => 
            sp.GetRequiredService<IMultiTenantContextAccessor<TTenantInfo>>().MultiTenantContext!);

        services.AddScoped<TTenantInfo>(sp => 
            sp.GetRequiredService<IMultiTenantContextAccessor<TTenantInfo>>().MultiTenantContext?.TenantInfo!);
        services.AddScoped<ITenantInfo>(sp => sp.GetService<TTenantInfo>()!);

        services.AddSingleton<IMultiTenantContextAccessor<TTenantInfo>, MultiTenantContextAccessor<TTenantInfo>>();
        services.AddSingleton<IMultiTenantContextAccessor>(sp => 
            (IMultiTenantContextAccessor)sp.GetRequiredService<IMultiTenantContextAccessor<TTenantInfo>>());

        return new MultiTenantBuilder<TTenantInfo>(services);
    } 
}