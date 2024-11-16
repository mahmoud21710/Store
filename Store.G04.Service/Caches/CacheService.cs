using StackExchange.Redis;
using Store.G04.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G04.Service.Caches
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<string> GetCachekeyAsync(string Key)
        {
            var cacheresponse = await _database.StringGetAsync(Key);
            if (cacheresponse.IsNullOrEmpty) return null; 
            return cacheresponse.ToString();
        }

        public async Task SetCacheKeyAsync(string key, object response, TimeSpan expiretime)
        {
            if (response is null) return;

            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };   // be cause in redis convention is Pascalcase , and iam need camelcase

            var response02 = JsonSerializer.Serialize(response, options);
            await _database.StringSetAsync(key, response02, expiretime); 
        }
    }
}
