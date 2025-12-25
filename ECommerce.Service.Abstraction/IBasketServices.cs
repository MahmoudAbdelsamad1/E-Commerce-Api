using ECommerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Abstraction
{
    public interface IBasketServices
    {
        public Task<BasketDTO> GetBasketAsync(string id );
        public Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket );
        public Task<bool> DeleteBasketAsync(string id );
    }
}
