using ECommerce.Service.Abstraction;
using ECommerce.Shared.DTOs.BasketDTOs;
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

    public class BasketsController : ControllerBase
    {
        private readonly IBasketServices _basketService;

        public BasketsController(IBasketServices basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        // Get :  BaseUrl/api/Baskets?id=
        public async Task<ActionResult<BasketDTO>> GetBasketAsync(string id)
        {
            return Ok(await _basketService.GetBasketAsync(id));
        }

        [HttpPost]
        // Post : BaseUrl/api/Baskets

        public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasket(BasketDTO basket) {

           var createdOrUpdatedBasket = await _basketService.CreateOrUpdateBasketAsync(basket);

            return Ok(createdOrUpdatedBasket);
        }


        [HttpDelete("{id}")]
        // Get : BaseUrl/api/Baskets/id
        public async Task<ActionResult<bool>> DeleteBasket(string id) {

            return Ok(await _basketService.DeleteBasketAsync(id));
        }
    }
}
