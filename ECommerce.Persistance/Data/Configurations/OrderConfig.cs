using ECommerce.Domain.Entities.OrderModule;
using ECommerce.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Percistance.Data.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)

        {

            builder.Property(X => X.SubTotal).HasPrecision(precision: 8, scale: 2);

            builder.OwnsOne(X => X.Address,  OEntity =>
{
                OEntity.Property(propertyExpression: X => X.FirstName).HasMaxLength(maxLength: 50);

                OEntity.Property(propertyExpression: X => X.LastName).HasMaxLength(maxLength: 50);

                OEntity.Property(propertyExpression: X => X.City).HasMaxLength(maxLength: 50);

                OEntity.Property(propertyExpression: X => X.Country).HasMaxLength(maxLength: 50);

                OEntity.Property(propertyExpression: X => X.Street).HasMaxLength(maxLength: 50);

            });

        }

        
    }
}
