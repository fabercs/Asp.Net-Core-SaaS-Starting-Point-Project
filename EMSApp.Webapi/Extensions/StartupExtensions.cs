using EMSApp.Core.Interfaces;
using EMSApp.Infrastructure.Data.DbContextConfig;
using EMSApp.Webapi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace EMSApp.Webapi.Extensions
{
    public static class StartupExtensions
    {
        public static void UseSetTenantContext(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<SetTenantContext>();
        }
        public static void UseEnsureMigrations(this IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var tenants = serviceScope.ServiceProvider.GetService<ITenantProvider>().GetTenants();
                if (tenants.Count == 0)
                    return;

                IDesignTimeDbContextFactory<EMSAppDbContext> dbContextFactory = (IDesignTimeDbContextFactory<EMSAppDbContext>)
                    serviceScope.ServiceProvider.GetService(typeof(IDesignTimeDbContextFactory<EMSAppDbContext>));

                var dummyDbContext = dbContextFactory.CreateDbContext(new string[] { });
                var dbUpdateExist = dummyDbContext.Database.GetPendingMigrations().Count() > 0;

                if (dbUpdateExist)
                {
                    foreach (var tenant in tenants)
                    {
                        var context = dbContextFactory.CreateDbContext(new string[] { tenant.Id.ToString(), tenant.ConnectionString });
                        if (context != null)
                        {
                            context.Database.Migrate();
                        }
                    }

                    PushLatestScriptToStorage(dummyDbContext.Database.GenerateCreateScript());
                }

            }
        }

        private static void PushLatestScriptToStorage(string latestSqlScript)
        {
            return;
        }

        public static IApplicationBuilder UseMissingTenantMiddleware(this IApplicationBuilder builder, string missingTenantUrl)
        {
            return builder.UseMiddleware<MissingTenantMiddleware>(missingTenantUrl);
        }


    }
}
