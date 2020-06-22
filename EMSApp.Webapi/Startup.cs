using AutoMapper;
using EMSApp.Core.DependencyInjection;
using EMSApp.Infrastructure.Data.DbContextConfig;
using EMSApp.Infrastructure.Data.DependencyInjection;
using EMSApp.Infrastructure.DependencyInjection;
using EMSApp.Webapi.DependencyInjection;
using EMSApp.Webapi.Extensions;
using EMSApp.Webapi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

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
            services.AddControllers(config => {
                //config.Filters.Add<TenantRequired>();
                //config.Filters.Add<Validate>();
            });
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.AddAutoMapper(typeof(Startup));
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddCors();

            services.AddCoreDependencies();
            services.AddInfrastructureDependencies();
            services.AddInfraDataDependencies(Configuration);
            services.AddApiDependencies(Configuration);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            app.SetTenantContextMiddleware();

            app.UseEnsureMigrations(); //TODO: refactor for development env.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
            });
            
        }
    }
}
