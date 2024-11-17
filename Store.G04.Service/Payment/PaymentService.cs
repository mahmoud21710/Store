using Microsoft.Extensions.Configuration;
using Store.G04.Core;
using Store.G04.Core.Entities;
using Store.G04.Core.Entities.OrderEntities;
using Store.G04.Core.Services.Contract;
using Store.G04.Core.Specifications.Orders;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Store.G04.Core.Entities.Product;

namespace Store.G04.Service.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymentService(IBasketService basketService , IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntentIdAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            //GetBasket 
            var basket =await _basketService.GetBsketByIdAsync(basketId);

            var shippinPrice = 0m;

            if (basket.DeliveryMethod.HasValue)
            {
                var delivery = await _unitOfWork.CreateRepository<DeliveryMethod, int>().GetAsync(basket.DeliveryMethod.Value);
                shippinPrice =delivery.Cost;
            }
            if (basket.Items.Count() > 0) 
            {
                foreach (var item in basket.Items) 
                {
                   var product =await  _unitOfWork.CreateRepository<Product, int>().GetAsync(item.Id);
                   if(item.Price != product.Price) 
                   {
                        item.Price = product.Price;
                   }
                }
            }

            var subTotal = basket.Items.Sum(I => I.Price * I.Quantity);

            var service = new PaymentIntentService();



            if (basket is null) return null;

            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId)) 
            {
                //create 
                var option = new PaymentIntentCreateOptions() 
                {
                    Amount =(long)( subTotal *100 + shippinPrice *100),
                    PaymentMethodTypes = new List<string>() {"card" },
                    Currency="usd"
                };
                paymentIntent =await service.CreateAsync(option);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else 
            {
                //Update 
                var option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(subTotal * 100 + shippinPrice * 100),                 
                };
                paymentIntent = await service.UpdateAsync(basket.PaymentIntentId, option);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }

            basket = await _basketService.UpdateCreateBasketAsync(basket);

            if(basket is null  ) return null;

            return basket;
            
        }

        public async Task<Order> UpdatePaymentIntentForSucceedOrFalid(string paymentIntentId, bool flag)
        {
            var srec = new OrderSpecificationWithPaymentId(paymentIntentId);
            var order = await _unitOfWork.CreateRepository<Order, int>().GetWithSpecAsync(srec);
            if (flag) 
            {
                order.Status = OrderStatus.PaymentReceived;
            }
            else
            {
                order.Status = OrderStatus.PaymentFailed;
            }

            _unitOfWork.CreateRepository<Order, int>().Update(order); 

            _unitOfWork.CompleteAsync();

            return order;
        }
    }
}
