using Store.G04.Core.Entities;
using Store.G04.Core.Entities.OrderEntities;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Services.Contract
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntentIdAsync(string basketId);

        Task<Order> UpdatePaymentIntentForSucceedOrFalid(string paymentIntentId ,bool flag);
    }
}
