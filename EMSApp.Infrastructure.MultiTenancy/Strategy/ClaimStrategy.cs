using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace EMSApp.Infrastructure.MultiTenancy;

public class ClaimStrategy : IMultiTenantStrategy
{
    private readonly string _claimKey;
    
    public ClaimStrategy(string claimKey)
    {
        if (string.IsNullOrWhiteSpace(claimKey))
        {
            throw new MultiTenantException("headerName can not be null or whitespace");
        }
        _claimKey = claimKey;
    }
    public Task<string?> GetIdentifierAsync(object context)
    {
        if (context is not HttpContext httpContext)
        {
            throw new MultiTenantException("Context must be an HttpContext object!");
        }
        var identifier = httpContext.User.FindFirstValue(_claimKey);
        return Task.FromResult<string?>(identifier);
    }
}