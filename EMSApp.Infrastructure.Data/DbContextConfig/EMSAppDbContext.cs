using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;
using EMSApp.Infrastructure.Data.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace EMSApp.Infrastructure.Data.DbContextConfig
{
    public class EMSAppDbContext : DbContext
    {
        private readonly Tenant _tenant;

        public DbSet<Fair> Fairs { get; set; }
            

        private static MethodInfo ConfigureGlobalFiltersMethodInfo;

        public EMSAppDbContext(DbContextOptions<EMSAppDbContext> options, ITenantProvider tenantProvider) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            _tenant = tenantProvider.GetCurrentTenant().Result;
           
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
                if (_tenant != null)
                    optionsBuilder.UseNpgsql(_tenant?.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder.Model != null)
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {

                    ConfigureGlobalFiltersMethodInfo
                        .MakeGenericMethod(entityType.ClrType)
                        .Invoke(this, new object[] { modelBuilder, entityType });
                }
            }


            base.OnModelCreating(modelBuilder);
        }


        #region Settings
        protected void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType)
            where TEntity : class
        {
            if (entityType.BaseType != null || !ShouldFilterEntity<TEntity>(entityType))
                return;

            var filterExpression = CreateFilterExpression<TEntity>();
            if (filterExpression == null) return;
            if (entityType.IsKeyless)
                modelBuilder.Entity<TEntity>().HasNoKey().HasQueryFilter(filterExpression);
            else
                modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
        }

        protected virtual bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
        {
            return typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity));
        }

        protected Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>() where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> removedFilter = e => !((ISoftDelete)e).IsDeleted;
                expression = expression == null ? removedFilter : CombineExpressions(expression, removedFilter);
            }

            return expression;
        }

        protected Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            return ExpressionCombiner.Combine(expression1, expression2);
        }
        #endregion Settings
    }
}
