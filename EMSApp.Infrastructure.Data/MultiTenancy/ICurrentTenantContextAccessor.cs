namespace EMSApp.Infrastructure.Data.MultiTenancy
{
    /// <summary>
    /// This class to get current tenant fromm all levels of a request.
    /// Mimics IHttpContextAccessor https://github.com/aspnet/HttpAbstractions/blob/master/src/Microsoft.AspNetCore.Http/HttpContextAccessor.cs#L10
    /// </summary>
    public interface ICurrentTenantContextAccessor
    {
        TenantContext CurrentTenant { get; set; }
    }
}
