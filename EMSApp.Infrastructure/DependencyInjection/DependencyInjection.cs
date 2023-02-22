using EMSApp.Core.Interfaces;
using EMSApp.Infrastructure.Auth;
using EMSApp.Infrastructure.Providers;
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
            serviceCollection.AddScoped<IErrorProvider, LocalizedErrorProvider>();
            return serviceCollection;
        }
    }
}
