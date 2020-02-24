using EMSApp.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EMSApp.Core.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAccountService, AccountService>();
            serviceCollection.AddScoped<IAuthService, AuthService>();

            return serviceCollection;
        }
    }
}
