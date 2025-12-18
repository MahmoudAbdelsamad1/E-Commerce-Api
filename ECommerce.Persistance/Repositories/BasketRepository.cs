using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.BasketModule;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Percistance.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _dataBase;

        public BasketRepository(IConnectionMultiplexer connection)
        {
            _dataBase = connection.GetDatabase();
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasket(CustomerBasket item, TimeSpan timeToLive)
        {
            var jsonItem = JsonSerializer.Serialize(item);
            var result = await _dataBase.StringSetAsync(item.Id, jsonItem);
            if (result)
            {

                return (await GetBasket(item.Id));

            }
            else
            {

                return null;
            }
        }

        public async Task<bool> DeleteBasket(string id)
        {
            bool deleted = await _dataBase.KeyDeleteAsync(id);
            return deleted;
        }

        public async Task<CustomerBasket?> GetBasket(string id)
        {
            var result = await _dataBase.StringGetAsync(id);

            if (result.IsNullOrEmpty)
            {

                return null;
            }
            else
            {

                return JsonSerializer.Deserialize<CustomerBasket>(result);
            }
        }
    }
}
