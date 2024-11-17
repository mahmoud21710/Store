using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.G04.Core.Dtos.Orders;
using Store.G04.Core.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Mapping.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile(IConfiguration configuration)
        {
            CreateMap<Order, OrdertoReturnDto>()
                   .ForMember(d => d.DeliveryMethod, option => option.MapFrom(s => s.DeliveryMethod.ShortName))
                   .ForMember(d => d.DeliveryMethodCost, option => option.MapFrom(s => s.DeliveryMethod.Cost));

            CreateMap<Address,AddressDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, option => option.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, option => option.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, option => option.MapFrom(s => $"{configuration["BASEURL"]}{s.Product.PictureUrl}"));
        }
    }
}
