using EMSApp.Core.Interfaces;
using EMSApp.Webapi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using EMSApp.Core.Services;

namespace EMSApp.Webapi.Extensions
{
    public static class StartupExtensions
    {
        public static void UseMultiTenancy(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<MultiTenantMiddleware>();
        }
        
        //TODO: Move this to Program.cs, only when app launches/re-launches
        public static async void UseEnsureMigrations(this IApplicationBuilder builder)
        {
            using var serviceScope = builder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var tenantService = serviceScope.ServiceProvider.GetService<ITenantService>();
            var tenants = await tenantService.GetAllTenants();
            if (!tenants.Any())
                return;

            IDesignTimeDbContextFactory<DbContext> dbContextFactory = (IDesignTimeDbContextFactory<DbContext>)
                serviceScope.ServiceProvider.GetService(typeof(IDesignTimeDbContextFactory<DbContext>));

            var dummyDbContext = dbContextFactory.CreateDbContext(null);
            var dbUpdateExist = (await dummyDbContext.Database.GetPendingMigrationsAsync()).Any();

            if (dbUpdateExist)
            {
                foreach (var tenant in tenants)
                {
                    var context = dbContextFactory.CreateDbContext(new string[]
                    {
                        tenant.Id.ToString(), 
                        tenant.ConnectionString
                    });
                    await context.Database.MigrateAsync();
                }

                PushLatestScriptToStorage(dummyDbContext.Database.GenerateCreateScript());
            }
        }
        
        private static void PushLatestScriptToStorage(string latestSqlScript)
        {
            using StreamWriter sw = new StreamWriter(@"D:\SqlScript\latest.sql");
            sw.Write(latestSqlScript);
        }
    }
}
