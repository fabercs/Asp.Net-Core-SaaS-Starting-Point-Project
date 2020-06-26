using EMSApp.Core.Entities;
using EMSApp.Infrastructure.Data.Helper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Action = EMSApp.Core.Entities.Action;

namespace EMSApp.Infrastructure.Data.DbContextConfig
{
    public class EMSHostDbContext : IdentityDbContext<ApplicationUser, 
        ApplicationRole, 
        Guid>
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantContact> TenantContacts { get; set; }
        public DbSet<TenantLicence> TenantLicences { get; set; }
        public DbSet<TenantSetting> TenantSettings { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<ApplicationRoleAction> ApplicationRoleActions { get; set; }
        public EMSHostDbContext(DbContextOptions<EMSHostDbContext> options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationRoleAction>().Ignore("Id");
            modelBuilder.Entity<ApplicationRoleAction>().
                HasKey(x => new { x.ApplicationRoleId, x.ActionId });
            modelBuilder.Entity<ApplicationRoleAction>()
                .HasOne(x => x.ApplicationRole)
                .WithMany(x => x.AppRoleActions)
                .HasForeignKey(x => x.ApplicationRoleId);
            modelBuilder.Entity<ApplicationRoleAction>()
                .HasOne(x => x.Action)
                .WithMany(x => x.AppRoleActions)
                .HasForeignKey(x => x.ActionId);

            modelBuilder.SeedDatabase();
        }
    }
}
