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
        async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            {

                var products = await _services.GetAllProductsAsync();

                return Ok(products);
            }

        }

        // Get : BaseUrl/api/Products/id

        [HttpGet("{id}")]
        async Task<ActionResult<ProductDTO>> GetProductByIdAsync(int id)
        {
            var product = await _services.GetProductsByIdAsync(id);

            return Ok(product);
        }


        // Get : BaseUrl/api/Products/brands

        [HttpGet("brands")]
        async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands(int id)
        {
            var brands = await _services.GetAllBrandsAsync();

            return Ok(brands);
        }

        // Get : BaseUrl/api/Products/types 

        [HttpGet("types")]
        async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes(int id)
        {
            var types = await _services.GetAllTypesAsync();

            return Ok(types);
        }
    }
}
