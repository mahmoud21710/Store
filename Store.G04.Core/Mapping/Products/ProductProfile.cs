using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Mapping.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Product,ProductDto>()
                .ForMember(D=>D.BrandName , options=>options.MapFrom(S=>S.Brand.Name))
                .ForMember(d => d.TypeName , options => options.MapFrom(s=>s.Type.Name))
                //.ForMember(D => D.PictureUrl , option => option.MapFrom(s => $"{configuration["BASEURL"]}{s.PictureUrl}"));
                .ForMember(D => D.PictureUrl , option => option.MapFrom(new PictureUrlResolver(configuration)));

            CreateMap<ProductBrand,TypeBrandDto>();
            CreateMap<ProductType,TypeBrandDto>();
        }
    }
}
