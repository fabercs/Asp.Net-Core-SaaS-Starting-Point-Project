using EMSApp.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EMSApp.Core.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<FairService, FairService>();
            serviceCollection.AddScoped<IEncryptionService, EncryptionService>();

            return serviceCollection;
        }
    }
}
