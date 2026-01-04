using ECommerce.Presintation.Attributes;
using ECommerce.Service.Abstraction.IProductServices;
using ECommerce.Shared;
using ECommerce.Shared.CommonResults;
using ECommerce.Shared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presintation.Controller
{

    public class ProductsController : ApiBaseController
    {
        private readonly IProductServices _services;

        public ProductsController(IProductServices services)
        {
            _services = services;
        }
        // Get : BaseUrl/api/Products

        [HttpGet]
        [RedisCache(5)]
        public  async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProducts([FromQuery] ProductQueryParams queryParams)
        {
            {

                var products = await _services.GetAllProductsAsync(queryParams);

                return Ok(products);
            }

        }

        // Get : BaseUrl/api/Products/id

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductByIdAsync(int id)
        {
           var result = await _services.GetProductsByIdAsync(id);

            return HandleResult<ProductDTO>(result);
        }


        // Get : BaseUrl/api/Products/brands

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands()
        {
            var brands = await _services.GetAllBrandsAsync();

            return Ok(brands);
        }

        // Get : BaseUrl/api/Products/types 

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes()
        {
            var types = await _services.GetAllTypesAsync();

            return Ok(types);
        }
    }
}
