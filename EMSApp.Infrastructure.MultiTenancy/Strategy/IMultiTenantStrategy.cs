namespace EMSApp.Infrastructure.MultiTenancy;

public interface IMultiTenantStrategy
{
    Task<string?> GetIdentifierAsync(object context);

    int Priority => 0;
}