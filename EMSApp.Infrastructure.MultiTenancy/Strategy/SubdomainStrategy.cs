using EMSApp.Shared.Extensions;
using Microsoft.AspNetCore.Http;

namespace EMSApp.Infrastructure.MultiTenancy;

public class SubdomainStrategy : IMultiTenantStrategy
{
    private readonly string _template;
    private static readonly string[] ProtocolPrefixes = { "http://", "https://" };
    
    public SubdomainStrategy(string template)
    {
        /*if (string.IsNullOrWhiteSpace(template))
        {
            throw new MultiTenantException(message:"Template can not be null or whitespace!");
        }*/
        
        _template = template;
    }
    public async Task<string?> GetIdentifierAsync(object context)
    {
        if (context is not HttpContext httpContext)
            throw new MultiTenantException("context must be an HttpContext object!");

        if (!httpContext.Request.Host.HasValue)
            return await Task.FromResult<string?>(null);

        var host = httpContext.Request.Host.Value.RemovePrefix(ProtocolPrefixes);
        //TODO: dynamically extract tenant identifier from the host with the given template

        var hostSections = host.Split('.');
        return await Task.FromResult(hostSections[0]);

    }
}