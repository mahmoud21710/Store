using AutoMapper;
using Store.G04.Core;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Entities;
using Store.G04.Core.Services.Contract;
using Store.G04.Core.Specifications;
using Store.G04.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(string? sort, int? brandId, int? typeId, int? pageSize, int? pageIndex)
        {
            var spec = new ProductSpecifications(sort,brandId,typeId,pageSize.Value,pageIndex.Value);
           return _mapper.Map<IEnumerable<ProductDto>>
            (await _unitOfWork.CreateRepository<Product, int>().GetAllWithSpecAsync(spec));
        }
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var spec = new ProductSpecifications(id);
            var product = await _unitOfWork.CreateRepository<Product, int>().GetWithSpecAsync(spec);
            var mappedproduct = _mapper.Map<ProductDto>(product);
            return mappedproduct;
        }


        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync() =>
            _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.CreateRepository<ProductType, int>().GetAllAsync());


        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
           return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.CreateRepository<ProductBrand, int>().GetAllAsync());
        }
        
    }
}
