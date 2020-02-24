using EMSApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMSApp.Infrastructure.Data.DbContextConfig
{
    public class EMSHostDbContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantContact> TenantContact { get; set; }
        public DbSet<TenantSetting> TenantSettings { get; set; }
        public EMSHostDbContext(DbContextOptions<EMSHostDbContext> options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
