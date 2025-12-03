using ECommerce.Domain.Entities.ProductModule;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product,int> 
    {
        public ProductWithBrandAndTypeSpecifications() : base()
        {

            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

        
    }
}
