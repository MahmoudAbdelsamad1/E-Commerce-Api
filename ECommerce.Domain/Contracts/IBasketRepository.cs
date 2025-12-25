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
        public Task<CustomerBasket?> GetBasket(string id);
        public Task<CustomerBasket?> CreateOrUpdateBasket(CustomerBasket item , TimeSpan timeToLive = default);
        public Task<bool> DeleteBasket(string id);

    }
}
