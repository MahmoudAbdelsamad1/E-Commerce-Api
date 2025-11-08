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
    public class BasEntityConfig<T> : IEntityTypeConfiguration<T> where T : BaseEntity<int>
    {
        public void Configure(EntityTypeBuilder<T> builder)
        { 
           builder.HasKey(X => X.Id);
        }
    }
}
