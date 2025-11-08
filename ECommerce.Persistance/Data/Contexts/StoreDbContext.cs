using ECommerce.Domain.Data.Configurations;
using ECommerce.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Percistance.Data.Contexts
{
    public class StoreDbContext :DbContext
    {

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=ECommerce;Trusted_Connection=true;TrustServerCertificate=True");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfig).Assembly);


        }

        public DbSet<Product> Products { get; set; } // 
        public DbSet<ProductBrand> ProductBrands { get; set; } // 
        public DbSet<ProductType> ProductTypes { get; set; } // 
    }
}

