using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Abstraction.ICacheService
{
    public interface ICacheService
    {
        public Task<string?> GetAsync(string cacheKey);

        public Task SetAsync(string cacheKey, object cacheValue, TimeSpan timeToLive);
    }
}
