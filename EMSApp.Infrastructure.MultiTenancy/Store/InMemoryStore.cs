using System.Collections.Concurrent;
using Microsoft.Extensions.Options;

namespace EMSApp.Infrastructure.MultiTenancy;

public class InMemoryStore<TTenantInfo> : IMultiTenantStore<TTenantInfo> where TTenantInfo :
    class, ITenantInfo, new()
{
    private readonly ConcurrentDictionary<string, TTenantInfo> _tenantDict;

    public InMemoryStore(IOptions<InMemoryStoreOptions<TTenantInfo>> options)
    {
        var memoryStoreOptions = options.Value ?? new InMemoryStoreOptions<TTenantInfo>();
        _tenantDict = new ConcurrentDictionary<string, TTenantInfo>();
        
        foreach (var tenant in memoryStoreOptions.Tenants)
        {
            if(tenant.Id == null || tenant.Id == Guid.Empty)
                throw new MultiTenantException("Missing tenant id in options.");
            if(string.IsNullOrWhiteSpace(tenant.Identifier))
                throw new MultiTenantException("Missing tenant identifier in options.");
            if(_tenantDict.ContainsKey(tenant.Identifier))
                throw new MultiTenantException("Duplicate tenant identifier in options.");
            
            _tenantDict.TryAdd(tenant.Identifier, tenant);
        }
    }
    public async Task<TTenantInfo?> GetByIdentifierAsync(string identifier)
    {
        _tenantDict.TryGetValue(identifier, out var tenant);
        return await Task.FromResult(tenant);
    }

    public async Task<TTenantInfo?> GetByIdAsync(Guid id)
    {
        var result = _tenantDict.Values.SingleOrDefault(t => t.Id == id);
        return await Task.FromResult(result);
    }

    public async Task<IEnumerable<TTenantInfo>> GetAllAsync()
    {
        return await Task.FromResult(_tenantDict.Values);
    }
}