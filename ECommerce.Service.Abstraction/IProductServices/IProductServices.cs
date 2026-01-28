using ECommerce.Shared;
using ECommerce.Shared.CommonResults;
using ECommerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Abstraction.IProductServices
{
    public interface IProductServices
    {
        Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParamse);
        Task<Result< ProductDTO?>> GetProductsByIdAsync(int Id);

        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();

        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();

    }
}
