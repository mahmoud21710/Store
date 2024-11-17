using Store.G04.Core;
using Store.G04.Core.Entities;
using Store.G04.Core.Entities.OrderEntities;
using Store.G04.Core.Services.Contract;
using Store.G04.Core.Specifications.Orders;
using Store.G04.Ropository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Service.Services.Oreders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork , IBasketService basketService,IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _basketService = basketService;
            _paymentService = paymentService;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int delivreyMethodId, Address shippingAddress)
        {
            var basket =await  _basketService.GetBsketByIdAsync(basketId);

            if (basket is  null) return null;

            var orderItems = new List<OrderItem>();

            if (basket.Items.Count() > 0) 
            {
                foreach (var item in basket.Items) 
                {
                    var product = await _unitOfWork.CreateRepository<Product, int>().GetAsync(item.Id);
                    var ProductOrderItem = new ProductItemOrder(product.Id, product.Name,product.PictureUrl);
                    var orderItem = new OrderItem(ProductOrderItem, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }

            var deliveryMeyhod = await _unitOfWork.CreateRepository<DeliveryMethod, int>().GetAsync(delivreyMethodId);

            var subTotal = orderItems.Sum(I => I.Price * I.Quantity);

            //TODOOOO
            if(!string.IsNullOrEmpty(basket.PaymentIntentId)) 
            {
                var spec = new OrderSpecificationWithPaymentId(basket.PaymentIntentId);

                var Exorder =await _unitOfWork.CreateRepository<Order, int>().GetWithSpecAsync(spec);

                _unitOfWork.CreateRepository<Order, int>().Delete(Exorder);
            }

           

             var basketDto =await _paymentService.CreateOrUpdatePaymentIntentIdAsync(basketId);

            var order = new Order(buyerEmail, shippingAddress, deliveryMeyhod, orderItems, subTotal, basketDto.PaymentIntentId);

            await _unitOfWork.CreateRepository<Order, int>().AddAsync(order);

            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0) return null;

            return order;
        }

        public async Task<IEnumerable<Order>?> GetOrdersForSpecificUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail);
            var order = await _unitOfWork.CreateRepository<Order, int>().GetAllWithSpecAsync(spec);
            if (order is null) return null;
            return order;
        }

        public async Task<Order>? GetOrdersByIdForSpecificUserAsync(string buyerEmail, int orderId)
        {
            var spec = new OrderSpecification(buyerEmail , orderId);
            var order = await _unitOfWork.CreateRepository<Order, int>().GetWithSpecAsync(spec);
            if (order is null) return null;
            return order;
        }
    }
}
