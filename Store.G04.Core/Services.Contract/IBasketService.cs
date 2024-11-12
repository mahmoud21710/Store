using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Services.Contract
{
    public interface IBasketService
    {
        Task<CustomerBasket> GetBsketByIdAsync(string bsketId); 
        Task<CustomerBasket> UpdateCreateBasketAsync(CustomerBasket basket); 
        Task<bool> DeleteBasket(string basketId);
    }
}
