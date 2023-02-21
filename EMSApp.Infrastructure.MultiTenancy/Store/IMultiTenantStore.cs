using EMSApp.Infrastructure.MultiTenancy;

namespace EMSApp.Infrastructure.MultiTenancy;

public interface IMultiTenantStore<T> where T : ITenantInfo, new()
{
    Task<T?> GetByIdentifierAsync(string identifier);

    Task<T?> GetByIdAsync(Guid id);

    Task<IEnumerable<T>> GetAllAsync();
}