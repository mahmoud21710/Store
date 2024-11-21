using AdminDashBoard.Models;
using AutoMapper;
using Store.G04.Core.Entities;

namespace AdminDashBoard.Helper
{
    public class MapsProfile :Profile
    {
        public MapsProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
