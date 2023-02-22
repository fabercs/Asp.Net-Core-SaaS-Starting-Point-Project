using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;
using EMSApp.Infrastructure.Data.Helper;
using EMSApp.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq.Expressions;
using System.Reflection;
using EMSApp.Infrastructure.MultiTenancy;

namespace EMSApp.Infrastructure.Data.DbContextConfig
{
    public class EMSAppDbContext : DbContext
    {
        private readonly ITenantInfo _tenantInfo;
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        
        private static MethodInfo ConfigureGlobalFiltersMethodInfo;

        public EMSAppDbContext(DbContextOptions<EMSAppDbContext> options, ITenantInfo tenantInfo) : base(options)
        {
            _tenantInfo = tenantInfo;
        }
        public EMSAppDbContext(DbContextOptions<EMSAppDbContext> options) : base(options)
        {
            ConfigureGlobalFiltersMethodInfo = typeof(EMSAppDbContext)
              .GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (_tenantInfo != null && !string.IsNullOrWhiteSpace(_tenantInfo.ConnectionString))
                {
                    optionsBuilder.UseNpgsql(_tenantInfo.ConnectionString);
                }
            }

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IEntity).IsAssignableFrom(entityType.GetType()))
                {
                    ConfigureGlobalFiltersMethodInfo
                        .MakeGenericMethod(entityType.ClrType)
                        .Invoke(this, new object[] { modelBuilder, entityType });
                }
                    
            }

            modelBuilder.Entity<Country>().HasNoKey().HasIndex("Code");
            modelBuilder.Entity<City>().HasNoKey().HasIndex("Code");

            base.OnModelCreating(modelBuilder);
        }


        #region Settings
        protected void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType)
            where TEntity : IEntity
        {
            if (entityType.BaseType != null || !ShouldFilterEntity<IEntity>(entityType))
                return;

            var filterExpression = CreateFilterExpression<IEntity>();
            if (filterExpression == null) return;
            if (entityType.IsKeyless)
                modelBuilder.Entity<BaseEntity>().HasNoKey().HasQueryFilter(filterExpression);
            else
                modelBuilder.Entity<BaseEntity>().HasQueryFilter(filterExpression);
        }

        protected virtual bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
        {
            return typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity));
        }

        protected static Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>() where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> removedFilter = e => !((ISoftDelete)e).IsDeleted;
                expression = expression == null ? removedFilter : CombineExpressions(expression, removedFilter);
            }

            return expression;
        }

        protected static Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            return ExpressionCombiner.Combine(expression1, expression2);
        }
        #endregion Settings
    }
}
