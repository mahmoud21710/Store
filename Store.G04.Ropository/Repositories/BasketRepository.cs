using StackExchange.Redis;
using Store.G04.Core.Entities;
using Store.G04.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G04.Ropository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketid)
        {
            var basket = await _database.StringGetAsync(basketid);

            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketASync(CustomerBasket basket)
        {
            var createedOrUpdateedBasket = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if(createedOrUpdateedBasket is false) return null;
            return await GetBasketAsync(basket.Id);
        }
        public async Task<bool> DeleeteBasketASync(string basketid)
        {
            return await _database.KeyDeleteAsync(basketid);
        }
    }
}
