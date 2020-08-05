using EMSApp.Core.Interfaces;
using EMSApp.Webapi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;

namespace EMSApp.Webapi.Extensions
{
    public static class StartupExtensions
    {
        public static void SetTenantContextMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<TenantContextMiddleware>();
        }
        
        public static async void UseEnsureMigrations(this IApplicationBuilder builder)
        {
            using var serviceScope = builder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var tenants = await serviceScope.ServiceProvider.GetService<ITenantProvider>().GetTenants();
            if (tenants.Count == 0)
                return;

            IDesignTimeDbContextFactory<DbContext> dbContextFactory = (IDesignTimeDbContextFactory<DbContext>)
                serviceScope.ServiceProvider.GetService(typeof(IDesignTimeDbContextFactory<DbContext>));

            var dummyDbContext = dbContextFactory.CreateDbContext(null);
            var dbUpdateExist = dummyDbContext.Database.GetPendingMigrations().Count() > 0;

            if (dbUpdateExist)
            {
                foreach (var tenant in tenants)
                {
                    var context = dbContextFactory.CreateDbContext(new string[] { tenant.Id.ToString(), tenant.ConnectionString });
                    if (context != null)
                    {
                        await context.Database.MigrateAsync();
                    }
                }

                PushLatestScriptToStorage(dummyDbContext.Database.GenerateCreateScript());
            }
        }
        
        private static void PushLatestScriptToStorage(string latestSqlScript)
        {
            using (StreamWriter sw = new StreamWriter(@"D:\SqlScript\latest.sql"))
            {
                sw.Write(latestSqlScript);
            }
        }
        
        //TODO : do we need missing tenant middleware?
        public static IApplicationBuilder MissingTenantMiddleware(this IApplicationBuilder builder, string missingTenantUrl)
        {
            return builder.UseMiddleware<MissingTenantMiddleware>(missingTenantUrl);
        }
    }
}
