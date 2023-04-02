// <auto-generated>
// ReSharper disable All

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TakeAIMeal.Data
{
    // ****************************************************************************************************
    // This is not a commercial licence, therefore only a few tables/views/stored procedures are generated.
    // ****************************************************************************************************

    public class TakeAIMealDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<TakeAIMeal.Data.Entities.ApplicationUser, TakeAIMeal.Data.Entities.ApplicationRole, int>, ITakeAIMealDbContext
    {
        public TakeAIMealDbContext()
        {
        }

        public TakeAIMealDbContext(DbContextOptions<TakeAIMealDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } // Products
        public DbSet<ProductCategory> ProductCategories { get; set; } // ProductCategories
        public DbSet<Receipe> Receipes { get; set; } // Receipes
        public DbSet<UserDiet> UserDiets { get; set; } // UserDiets
        public DbSet<UserProductsExclusion> UserProductsExclusions { get; set; } // UserProductsExclusions

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=tcp:sqlsrv-take-ai-meal.database.windows.net,1433;Initial Catalog=sqldb-take-ai-meal;Persist Security Info=False;User ID=takeaimeal-admin;Password=6m9Aa5E53we3dqC;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");
            }
        }

        public bool IsSqlParameterNull(SqlParameter param)
        {
            var sqlValue = param.SqlValue;
            var nullableValue = sqlValue as INullable;
            if (nullableValue != null)
                return nullableValue.IsNull;
            return (sqlValue == null || sqlValue == DBNull.Value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ReceipeConfiguration());
            modelBuilder.ApplyConfiguration(new UserDietConfiguration());
            modelBuilder.ApplyConfiguration(new UserProductsExclusionConfiguration());
        }

    }
}
// </auto-generated>
