using Microsoft.Extensions.DependencyInjection;

namespace EMSApp.Infrastructure.MultiTenancy;

public static partial class MultiTenantBuilderExtensions
{
    public static MultiTenantBuilder<TTenantInfo> WithHeaderStrategy<TTenantInfo>(this MultiTenantBuilder<TTenantInfo> builder)
        where TTenantInfo : class, ITenantInfo, new()
    {
        return builder.WithStrategy<HeaderStrategy>(ServiceLifetime.Scoped, MultiTenantConstants.TENANT_CODE);
    }
    
    public static MultiTenantBuilder<TTenantInfo> WithHeaderStrategy<TTenantInfo>(this MultiTenantBuilder<TTenantInfo> builder,
        string headerKey)
        where TTenantInfo : class, ITenantInfo, new()
    {
        return builder.WithStrategy<HeaderStrategy>(ServiceLifetime.Scoped, headerKey);
    }

    public static MultiTenantBuilder<TTenantInfo> WithStaticStrategy<TTenantInfo>(
        this MultiTenantBuilder<TTenantInfo> builder,
        string tenantIdentifier)
        where TTenantInfo : class, ITenantInfo, new()
    {
        return builder.WithStrategy<StaticStrategy>(ServiceLifetime.Scoped, tenantIdentifier);
    }

    public static MultiTenantBuilder<TTenantInfo> WithClaimStrategy<TTenantInfo>(
        this MultiTenantBuilder<TTenantInfo> builder)
        where TTenantInfo : class, ITenantInfo, new()
    {
        return builder.WithStrategy<ClaimStrategy>(ServiceLifetime.Scoped);
    }
    
    public static MultiTenantBuilder<TTenantInfo> WithClaimStrategy<TTenantInfo>(
        this MultiTenantBuilder<TTenantInfo> builder, string claimKey)
        where TTenantInfo : class, ITenantInfo, new()
    {
        return builder.WithStrategy<ClaimStrategy>(ServiceLifetime.Scoped, claimKey);
    }
    
    public static MultiTenantBuilder<TTenantInfo> WithSubdomainStrategy<TTenantInfo>(
        this MultiTenantBuilder<TTenantInfo> builder)
        where TTenantInfo : class, ITenantInfo, new()
    {
        return builder.WithStrategy<SubdomainStrategy>(ServiceLifetime.Scoped);
    }
    
    public static MultiTenantBuilder<TTenantInfo> WithSubdomainStrategy<TTenantInfo>(
        this MultiTenantBuilder<TTenantInfo> builder, string template)
        where TTenantInfo : class, ITenantInfo, new()
    {
        return builder.WithStrategy<SubdomainStrategy>(ServiceLifetime.Scoped, template);
    }
    
    
}