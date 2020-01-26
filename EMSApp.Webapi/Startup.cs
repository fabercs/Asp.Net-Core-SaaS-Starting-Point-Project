using EMSApp.Core.Interfaces;
using EMSApp.Infrastructure.Data;
using EMSApp.Infrastructure.Data.DbContextConfig;
using EMSApp.Infrastructure.Data.MultiTenancy;
using EMSApp.Webapi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EMSApp.Webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddDbContext<EMSAppDbContext>(options => {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); 
            });


            services.AddScoped<ITenantProvider, SqlServerTenantProvider>();
            services.AddScoped<ICurrentTenantContextAccessor, CurrentTenantContextAccessor>();
            services.AddScoped(typeof(IDesignTimeDbContextFactory<EMSAppDbContext>), typeof(EMSAppDbContextFactory));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthorization();

            app.UseMissingTenantMiddleware("http://localhost:8999");

            app.UseSetTenantContext();

            app.UseEnsureMigrations(); //TODO: refactor for development env.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
