using ECommerce.Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Percistance.Data.Configurations
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {

        public void Configure(EntityTypeBuilder<OrderItem> builder)

        {

            builder.Property(X => X.Price).HasPrecision(precision: 8, scale: 2);

            builder.OwnsOne(navigationExpression: X => X.Product, buildAction: OEntity =>

            {

                OEntity.Property(X => X.ProductName).HasMaxLength(maxLength: 100);

                OEntity.Property(propertyExpression: X => X.PictureUrl).HasMaxLength(maxLength: 200);

            });

         }
    }
}
