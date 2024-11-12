using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Repositories.Contract
{
    public interface IBasketRepository
    {
        public Task<CustomerBasket?> GetBasketAsync(string basketid);
        public Task<CustomerBasket?> UpdateBasketASync(CustomerBasket basket); // Set = Update Or Create
        public Task<bool> DeleeteBasketASync(string basketid);
    }
}
