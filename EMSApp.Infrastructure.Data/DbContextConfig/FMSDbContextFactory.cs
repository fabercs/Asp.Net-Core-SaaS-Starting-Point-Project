using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace EMSApp.Infrastructure.Data.DbContextConfig
{
    public class EMSAppDbContextFactory : IDesignTimeDbContextFactory<EMSAppDbContext>
    {
        private readonly ILogger<EMSAppDbContextFactory> _logger;
        public EMSAppDbContextFactory(){}
        public EMSAppDbContextFactory(ILogger<EMSAppDbContextFactory> logger)
        {
            _logger = logger;
            
        }
        public EMSAppDbContext CreateDbContext(string[] args)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddJsonFile("appsettings.json", optional: false)
               .AddJsonFile($"appsettings.{envName}.json", optional: false)
               .Build();

            var builder = new DbContextOptionsBuilder<EMSAppDbContext>();

            if (args.Length > 1)
            {
                var connectionString = args[1];
                var tenantId = args[0];

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    _logger.LogError($"ConnectionString is not found for {tenantId}");
                    return null;
                }
                builder.UseSqlServer(connectionString);
            }
            else
            {
                var connString = configuration.GetConnectionString("TenantDevConnectionString"); //dev environment, add-migration,remove-migration cases..
                builder.UseSqlServer(connString);
            }

            return new EMSAppDbContext(builder.Options);
        }
    }
}
