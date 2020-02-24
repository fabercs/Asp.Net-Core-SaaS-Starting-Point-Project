using EMSApp.Core.Interfaces;
using EMSApp.Infrastructure.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace EMSApp.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITokenFactory, TokenFactory>();
            serviceCollection.AddScoped<IEmailService, EmailService>();
            serviceCollection.AddScoped<IJwtAuthResponseFactory, JwtAuthResponseFactory>();

            return serviceCollection;
        }
    }
}
