using Microsoft.AspNetCore.Http;

namespace EMSApp.Infrastructure.MultiTenancy;

public class HeaderStrategy : IMultiTenantStrategy
{
    private readonly string _headerKey;
    
    public HeaderStrategy(string headerName)
    {
        if (string.IsNullOrWhiteSpace(headerName))
        {
            throw new MultiTenantException("headerName can not be null or whitespace");
        }
        _headerKey = headerName;
    }
    public Task<string?> GetIdentifierAsync(object context)
    {
        if (context is not HttpContext httpContext)
        {
            throw new MultiTenantException("Context must be an HttpContext object!");
        }
        var headerValue = httpContext?.Request.Headers[_headerKey].FirstOrDefault();
        return Task.FromResult<string?>(headerValue);
    }
}