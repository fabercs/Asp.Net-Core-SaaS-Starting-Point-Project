using EMSApp.Core.Entities;
using EMSApp.Infrastructure.Data.Helper;
using EMSApp.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Reflection;
using Action = EMSApp.Core.Entities.Action;
using Module = EMSApp.Core.Entities.Module;

namespace EMSApp.Infrastructure.Data.DbContextConfig
{
    public class EMSHostDbContext : IdentityDbContext<ApplicationUser,
        ApplicationRole,
        Guid>
    {
        private static MethodInfo ConfigureGlobalFiltersMethodInfo;

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Licence> Licences { get; set; }
        public DbSet<TenantContact> TenantContacts { get; set; }
        public DbSet<TenantContactToken> TenantContactTokens { get; set; }
        public DbSet<TenantLicence> TenantLicences { get; set; }
        public DbSet<LicenceModule> LicenceModules { get; set; }
        public DbSet<TenantSetting> TenantSettings { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<ApplicationRoleAction> ApplicationRoleActions { get; set; }
        public EMSHostDbContext(DbContextOptions<EMSHostDbContext> options) : base(options)
        {
            ConfigureGlobalFiltersMethodInfo = typeof(EMSAppDbContext)
              .GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationRole>()
                .HasIndex("NormalizedName")
                .HasDatabaseName("RoleNameIndex")
                .IsUnique(false);

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

            modelBuilder.Entity<LicenceModule>().Ignore("Id");
            modelBuilder.Entity<LicenceModule>().
                HasKey(x => new { x.LicenceId, x.ModuleId });
            modelBuilder.Entity<LicenceModule>()
                .HasOne(x => x.Licence)
                .WithMany(x => x.LicenceModules)
                .HasForeignKey(x => x.LicenceId);
            modelBuilder.Entity<LicenceModule>()
                .HasOne(x => x.Module)
                .WithMany(x => x.LicenceModules)
                .HasForeignKey(x => x.ModuleId);

            modelBuilder.SeedDatabase();
        }

        protected static void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType)
            where TEntity : IEntity
        {
        }
    }
}
