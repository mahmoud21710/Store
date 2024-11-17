using Store.G04.Core.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Specifications.Orders
{
    public class OrderSpecificationWithPaymentId :BaseSpecifications<Order,int>
    {
        public OrderSpecificationWithPaymentId(string PaymentIntentId) : base(O =>O.PaymentIntentId ==PaymentIntentId)
        {
            IncludeS.Add(O => O.DeliveryMethod);
            IncludeS.Add(O => O.Items);
        }
    }
}
