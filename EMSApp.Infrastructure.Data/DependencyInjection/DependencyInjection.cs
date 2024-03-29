﻿using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;
using EMSApp.Infrastructure.Data.DbContextConfig;
using EMSApp.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EMSApp.Infrastructure.Data.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraDataDependencies(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            
            serviceCollection.AddDbContext<EMSHostDbContext>(opt => {
                opt.UseNpgsql(configuration.GetConnectionString("HostConnectionString"));
            });
            serviceCollection.AddDbContext<EMSAppDbContext>();

            var identityBuilder = serviceCollection.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
            })
            .AddEntityFrameworkStores<EMSHostDbContext>()
            .AddDefaultTokenProviders()
            .AddErrorDescriber<MutliLangErrorDescriber>();
            
            serviceCollection.AddScoped(typeof(IDesignTimeDbContextFactory<DbContext>), typeof(EMSAppDbContextFactory));
            serviceCollection.AddScoped(typeof(IAppRepository), typeof(EfRepository<EMSAppDbContext>));
            serviceCollection.AddScoped(typeof(IHostRepository), typeof(EfRepository<EMSHostDbContext>));

            return serviceCollection;
        }
    }
}
