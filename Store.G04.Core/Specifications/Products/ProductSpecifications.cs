using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Specifications.Products
{
    public class ProductSpecifications  :BaseSpecifications<Product,int>
    {
        public ProductSpecifications(int id):base(P => P.Id == id)
        {
            //IncludeS.Add(P => P.Brand);
            //IncludeS.Add(P => P.Type);
            ApplyIncludes();
        }
        //900
        //P.Z=50
        //P.I=2
        public ProductSpecifications(ProductSpecParams productSpecParams) :base(
            P => 
            (string.IsNullOrEmpty(productSpecParams.Search) || P.Name.ToLower().Contains(productSpecParams.Search))
            &&
            (!productSpecParams.BrandId.HasValue || productSpecParams.BrandId == P.BrandId ) 
            &&
            (!productSpecParams.TypeId.HasValue || productSpecParams.TypeId == P.TypeId )
            )
        {
            //IncludeS.Add(P => P.Brand);
            //IncludeS.Add(P => P.Type);

            

            //name ,priceAsc ,priceDesc

            if (!string.IsNullOrEmpty(productSpecParams.Sort)) 
            {
                switch (productSpecParams.Sort) 
                {                                     
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }

            ApplyIncludes();

            
            ApplyPagination((productSpecParams .PageIndex- 1)* productSpecParams.PageSize, productSpecParams.PageSize);
        }
        private void ApplyIncludes() 
        {
            IncludeS.Add(P => P.Brand);
            IncludeS.Add(P => P.Type);
        }
    }
}
