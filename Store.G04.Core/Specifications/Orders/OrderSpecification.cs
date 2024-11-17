using Store.G04.Core.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Specifications.Orders
{
    public class OrderSpecification :BaseSpecifications<Order,int>
    {
        public OrderSpecification(string buyerEmail , int orderId) 
            :base(O=>O.ByerEmail==buyerEmail && O.Id==orderId)
        {
            ApplyIncludes();
        }
        public OrderSpecification(string buyerEmail ) 
            :base(O=>O.ByerEmail==buyerEmail)
        {
            ApplyIncludes();
        }

        private  void ApplyIncludes() 
        {
            IncludeS.Add(O => O.DeliveryMethod);
            IncludeS.Add(O => O.Items);
        }
    }
}
