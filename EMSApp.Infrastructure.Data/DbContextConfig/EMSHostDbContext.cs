using EMSApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace EMSApp.Infrastructure.Data.DbContextConfig
{
    public class EMSHostDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantContact> TenantContacts { get; set; }
        public DbSet<TenantLicence> TenantLicences { get; set; }
        public DbSet<TenantSetting> TenantSettings { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public EMSHostDbContext(DbContextOptions<EMSHostDbContext> options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Appadmin", NormalizedName = "Appadmin".ToUpper() }
                );
        }
    }
}
