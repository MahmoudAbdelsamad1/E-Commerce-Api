using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Tar;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product,int>  
    {
        public ProductWithBrandAndTypeSpecifications(int criteria  ) : base(P =>P.Id == criteria)
        {

            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

        }


        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams) : base( P => (queryParams.TypeId == null ||queryParams.TypeId == P.TypeId )
         && (queryParams.BrandId == null || queryParams.BrandId == P.BrandId ) && (queryParams.Search == null || P.Name.ToLower().Contains(queryParams.Search.ToLower())))
        {

            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);



            switch(queryParams.Sort){

                case ProductSortByOptions.NameAsc:
                    AddOrderByAscending(P => P.Name);
                        break ;
                case ProductSortByOptions.NameDes:
                    AddOrderByDescending(P => P.Name);
                    break;
                case ProductSortByOptions.PriceAsc:
                    AddOrderByAscending(P => P.Price);
                    break;
                case ProductSortByOptions.PriceDes:
                    AddOrderByDescending(P => P.Price);
                    break;
                default:
                    AddOrderByAscending(p=>p.Id);
                    break;





            }
        

                AddPagination(queryParams.PageIndex, queryParams.PageSize);
            



        }


    }
}
