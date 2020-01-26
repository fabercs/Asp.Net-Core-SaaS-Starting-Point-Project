using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EMSApp.Infrastructure.Data.DbContextConfig
{
    public class EMSAppDbContext : DbContext
    {
        private readonly Tenant _tenant;

        public DbSet<Fair> Fairs { get; set; }
        public DbSet<User> Users { get; set; }
        public EMSAppDbContext(DbContextOptions<EMSAppDbContext> options, ITenantProvider tenantProvider): base(options)
        {
            _tenant = tenantProvider.GetCurrentTenant();
        }
        public EMSAppDbContext(DbContextOptions<EMSAppDbContext> options): base(options){}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_tenant?.ConnectionString); 
            }
            
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
