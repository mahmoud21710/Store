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
        public ProductSpecifications(string? sort, int? brandId, int? typeId, int pageSize, int pageIndex):base(
            P => 
            (!brandId.HasValue || brandId == P.BrandId ) 
            &&
            (!typeId.HasValue || typeId == P.TypeId )
            )
        {
            //IncludeS.Add(P => P.Brand);
            //IncludeS.Add(P => P.Type);

            

            //name ,priceAsc ,priceDesc

            if (!string.IsNullOrEmpty(sort)) 
            {
                switch (sort) 
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

            
            ApplyPagination((pageIndex-1)*pageSize,pageSize);
        }
        private void ApplyIncludes() 
        {
            IncludeS.Add(P => P.Brand);
            IncludeS.Add(P => P.Type);
        }
    }
}
