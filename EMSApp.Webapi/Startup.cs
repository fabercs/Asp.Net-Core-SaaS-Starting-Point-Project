using EMSApp.Core.DependencyInjection;
using EMSApp.Infrastructure.Data.DependencyInjection;
using EMSApp.Infrastructure.DependencyInjection;
using EMSApp.Webapi.DependencyInjection;
using EMSApp.Webapi.Extensions;
using EMSApp.Webapi.Filters;
using EMSApp.Webapi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using EMSApp.Infrastructure.MultiTenancy;

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
                config.Filters.Add(new Validate());
            });
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.AddAutoMapper(typeof(Startup));
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddCors();

            services.AddMultiTenancy<TenantInfo>()
                .WithHeaderStrategy()
                .WithEfCoreStore();
                
            services.AddCoreDependencies();
            services.AddInfrastructureDependencies();
            services.AddInfraDataDependencies(Configuration);
            services.AddApiDependencies(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            
            app.UseMultiTenancy();
            
            app.UseAuthentication();
            app.UseAuthorization();

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
            
            //app.UseEnsureMigrations(); //TODO: refactor for development env.

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                if (!env.IsDevelopment())
                {
                    endpoints.MapControllers();
                }
                else
                {
                    endpoints.MapControllers().AllowAnonymous();
                }
            });
        }
    }
}
