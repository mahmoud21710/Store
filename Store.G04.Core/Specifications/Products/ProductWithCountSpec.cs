using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Specifications.Products
{
    public class ProductWithCountSpec : BaseSpecifications<Product,int> 
    {
        public ProductWithCountSpec(ProductSpecParams productSpecParams) : base(
             P =>
             (string.IsNullOrEmpty(productSpecParams.Search) || P.Name.ToLower().Contains(productSpecParams.Search))
             &&
             (!productSpecParams.BrandId.HasValue || productSpecParams.BrandId == P.BrandId)
             &&
             (!productSpecParams.TypeId.HasValue || productSpecParams.TypeId == P.TypeId)
             )
        {
           
        }
    }
}
