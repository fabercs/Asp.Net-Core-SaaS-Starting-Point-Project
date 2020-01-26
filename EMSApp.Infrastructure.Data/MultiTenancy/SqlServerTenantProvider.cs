using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EMSApp.Infrastructure.Data.MultiTenancy
{
    public class SqlServerTenantProvider : ITenantProvider
    {
        private static List<Tenant> _tenants = new List<Tenant>();
        

        private Tenant _tenant;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SqlServerTenantProvider(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            LoadTenantsFromSqlServer(configuration.GetConnectionString("HostConnectionString"));
            
            
        }

        private void LoadTenantsFromSqlServer(string connectionString)
        {
            var commandText = @"select Id,Name,Host,ConnectionString from dbo.Tenant";
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(commandText, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tenant = new Tenant();
                        tenant.Id = Guid.Parse(reader["Id"].ToString());
                        tenant.Name = reader["Name"].ToString();
                        tenant.Host = reader["Host"].ToString();
                        tenant.ConnectionString= reader["ConnectionString"].ToString();
                        _tenants.Add(tenant);
                    }
                }
            }
        }

        public Tenant GetCurrentTenant()
        {
            var host = _httpContextAccessor.HttpContext.Request.Host.Value;
            var tenant = _tenants.FirstOrDefault(t => t.Host.ToLower() == host.ToLower());
            if (tenant != null)
            {
                _tenant = tenant;
            }
            return _tenant;
        }

        public List<Tenant> GetTenants() => _tenants;
    }
}
