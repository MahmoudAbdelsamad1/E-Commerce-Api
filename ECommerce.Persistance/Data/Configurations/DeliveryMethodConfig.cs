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
    public class DeliveryMethodConfig : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)

        {

            builder.Property(X => X.Price).HasPrecision(precision: 8, scale: 2);

            builder.Property(X => X.ShortName).HasMaxLength(maxLength: 50);

            builder.Property(X => X.DeliveryTime).HasMaxLength(maxLength: 50); builder.Property(propertyExpression: X => X.Description).HasMaxLength(maxLength: 100);
        }
    }
}
