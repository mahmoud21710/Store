using Store.G04.Core.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Services.Contract
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int delivreyMethodId, Address shippingAddress);
        Task<IEnumerable<Order>?> GetOrdersForSpecificUserAsync(string buyerEmail);
        Task<Order>? GetOrdersByIdForSpecificUserAsync(string buyerEmail , int orderId);
    }
}
