using EMSApp.Core.Services;
using EMSApp.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace EMSApp.Core.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<ITenantService, TenantService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IRoleService, RoleService>();
            serviceCollection.AddScoped<IEncryptionService, EncryptionService>();
            serviceCollection.AddScoped<ILazyServiceProvider, LazyServiceProvider>();

            return serviceCollection;
        }
    }
}
