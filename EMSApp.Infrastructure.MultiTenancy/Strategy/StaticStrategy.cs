using Microsoft.AspNetCore.Http;

namespace EMSApp.Infrastructure.MultiTenancy;

public class StaticStrategy : IMultiTenantStrategy
{
    private readonly string? _tenantIdentifier;
    public StaticStrategy(string tenantIdentifier)
    {
        _tenantIdentifier = tenantIdentifier;
    }
    public Task<string?> GetIdentifierAsync(object context)
    {
        return Task.FromResult(_tenantIdentifier);
    }
}