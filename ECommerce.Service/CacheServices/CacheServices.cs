using ECommerce.Domain.Contracts;
using ECommerce.Service.Abstraction.ICacheService;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Service.ICacheServices
{
    public class CacheServices : ICacheService
    {
        private readonly ICacheRepository _repo;
        public CacheServices(ICacheRepository repo)
        {
            _repo = repo;
        }
        public async Task<string?> GetAsync(string cacheKey)
        {
            return await _repo.GetAsync(cacheKey);
        }

        public async Task SetAsync(string cacheKey, object cacheValue, TimeSpan timeToLive)
        {
            var value = JsonSerializer.Serialize(cacheValue);
           await _repo.SetAsync(cacheKey, value, timeToLive);
        }
    }
}
