using ECommerce.Service.Abstraction.IProductServices;
using ECommerce.Shared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presintation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _services;

        public ProductsController(IProductServices services)
        {
            _services = services;
        }
        // Get : BaseUrl/api/Products

        [HttpGet]
        public  async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts(int? typeId, int? productId)
        {
            {

                var products = await _services.GetAllProductsAsync( typeId,  productId);

                return Ok(products);
            }

        }

        // Get : BaseUrl/api/Products/id

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductByIdAsync(int id)
        {
            var product = await _services.GetProductsByIdAsync(id);

            return Ok(product);
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
