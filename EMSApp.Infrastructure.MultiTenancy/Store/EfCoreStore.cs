using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;

namespace EMSApp.Infrastructure.MultiTenancy;

public class EfCoreStore<T> : IMultiTenantStore<T> where T : class, ITenantInfo, new()
{
    private readonly IHostRepository _repository;

    public EfCoreStore(IHostRepository repository)
    {
        _repository = repository;
    }

    public async Task<T?> GetByIdentifierAsync(string identifier)
    {
        var tenant = await _repository.GetFirstAsync<Tenant>(t => t.AppName == identifier);
        if (tenant == null) return await Task.FromResult<T?>(null);
        ITenantInfo? tenantInfo = new TenantInfo
        {
            Id = tenant.Id,
            Identifier = tenant.Host,
            Name = tenant.AppName,
            ConnectionString = tenant.ConnectionString
        };
        return await Task.FromResult((T?)tenantInfo);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var tenant = await _repository.GetFirstAsync<Tenant>(t => t.Id == id);
        if (tenant == null) return await Task.FromResult<T?>(null);
        ITenantInfo? tenantInfo = new TenantInfo
        {
            Id = tenant.Id,
            Identifier = tenant.Host,
            Name = tenant.AppName,
            ConnectionString = tenant.ConnectionString
        };
        return await Task.FromResult((T?)tenantInfo);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var tenantList = new List<T>();
        var tenants = await _repository.GetAllAsync<Tenant>();
        foreach (var tenant in tenants)
        {
            ITenantInfo? tenantInfo = new TenantInfo
            {
                Id = tenant.Id,
                Identifier = tenant.Host,
                Name = tenant.AppName,
                ConnectionString = tenant.ConnectionString
            };
            
            tenantList.Add((T)tenantInfo);
        }
        return await Task.FromResult(tenantList); 
    }
}