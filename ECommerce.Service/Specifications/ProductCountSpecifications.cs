using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications
{
    internal class ProductCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductCountSpecifications(ProductQueryParams queryParams) :base(P => (queryParams.TypeId == null ||queryParams.TypeId == P.TypeId )
         && (queryParams.BrandId == null || queryParams.BrandId == P.BrandId ) && (queryParams.Search == null || P.Name.ToLower().Contains(queryParams.Search.ToLower())))
        {

        }
    }
}
