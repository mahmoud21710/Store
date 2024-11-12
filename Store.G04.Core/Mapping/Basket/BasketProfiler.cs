using AutoMapper;
using Store.G04.Core.Dtos.Basket;
using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Mapping.Basket
{
    public class BasketProfiler : Profile
    {
        public BasketProfiler()
        {
            CreateMap<CustomerBasket,CustomerBasketDto>().ReverseMap();
        }
    }
}
