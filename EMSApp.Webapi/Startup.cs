using EMSApp.Core.DependencyInjection;
using EMSApp.Infrastructure.Data.DbContextConfig;
using EMSApp.Infrastructure.Data.DependencyInjection;
using EMSApp.Infrastructure.DependencyInjection;
using EMSApp.Webapi.DependencyInjection;
using EMSApp.Webapi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddMemoryCache();
            

            services.AddCoreDependencies();
            services.AddInfrastructureDependencies();
            services.AddInfraDataDependencies(Configuration);
            services.AddApiDependencies();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //app.MissingTenantMiddleware("http://localhost:8999");

            app.SetTenantContextMiddleware();

            app.UseEnsureMigrations(); //TODO: refactor for development env.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
