using ECommerce.Domain.Entities.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface  IBasketRepository
    {
        public Task<CustomerBasket?> GetBasket(int id);
        public Task<CustomerBasket?> CreateOrUpdateBasket(BasketItem item , TimeSpan timeToLive);
        public Task<bool> DeleteBasket(int id);

    }
}
