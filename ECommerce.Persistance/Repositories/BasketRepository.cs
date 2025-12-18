using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.BasketModule;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Percistance.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IConnectionMultiplexer _dataBase;

        public BasketRepository(IConnectionMultiplexer dataBase)
        {
            _dataBase = dataBase;
        }
        public Task<CustomerBasket?> CreateOrUpdateBasket(BasketItem item, TimeSpan timeToLive)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBasket(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerBasket?> GetBasket(int id)
        {
            throw new NotImplementedException();
        }
    }
}
