namespace EMSApp.Infrastructure.MultiTenancy;

public interface IMultiTenantContextAccessor
{
    IMultiTenantContext? MultiTenantContext { get; set; }
}

public interface IMultiTenantContextAccessor<T> where T : class, ITenantInfo, new()
{
    IMultiTenantContext<T>? MultiTenantContext { get; set; }
}