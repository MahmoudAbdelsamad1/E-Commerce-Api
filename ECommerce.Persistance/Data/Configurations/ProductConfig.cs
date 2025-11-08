using ECommerce.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Data.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(X => X.Name).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(X => X.Description).HasColumnType("varchar").HasMaxLength(500);
            builder.Property(X => X.PictureUrl).HasColumnType("varchar").HasMaxLength(200);
            builder.Property(X => X.Price).HasColumnType("decimal").HasPrecision(8,2);

            builder.HasOne(X => X.ProductBrand).WithMany().HasForeignKey(X=> X.BrandId);

            builder.HasOne(X => X.ProductType).WithMany().HasForeignKey(X => X.TypeId);
        }
    }
}
