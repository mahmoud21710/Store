using StackExchange.Redis;
using Store.G04.Core.Entities;
using Store.G04.Core.Repositories.Contract;
using Store.G04.Core.Services.Contract;
using Store.G04.Ropository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Service.Services.Basket
{
    public class BasketServices : IBasketService
    {
        
        private readonly IBasketRepository _basket;

        public BasketServices(IBasketRepository basket)
        {
            
               _basket = basket;
        }
        public async Task<CustomerBasket> GetBsketByIdAsync(string bsketId)
        {
            var basket = await _basket.GetBasketAsync(bsketId);
            if (basket is null) return null;
            return basket;
        }

        public async Task<CustomerBasket> UpdateCreateBasketAsync(CustomerBasket basket)
        {
            return await _basket.UpdateBasketASync(basket);
        }
        public async Task<bool> DeleteBasket(string basketId)
        {
            return await _basket.DeleeteBasketASync(basketId);
        }
    }
}
