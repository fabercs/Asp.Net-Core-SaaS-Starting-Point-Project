namespace EMSApp.Core.Interfaces
{ 
    /// <summary>
    /// This class to get current tenant from all levels of a request.
    /// Mimics IHttpContextAccessor 
    /// https://github.com/aspnet/HttpAbstractions/blob/master/src/Microsoft.AspNetCore.Http/HttpContextAccessor.cs#L10
    /// </summary>
    public interface ICurrentTenantContextAccessor
    {
        ITenantContext CurrentTenant { get; set; }
    }
}
