using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities.ProductModule
{
    public abstract class BaseEntity<TEntity>
    {
        public TEntity Id { get; set; } = default!;
    }
}
