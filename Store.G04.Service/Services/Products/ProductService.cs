using AutoMapper;
using Store.G04.Core;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Entities;
using Store.G04.Core.Helper;
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
        public async Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecParams productSpecParams)
        {
            var spec = new ProductSpecifications(productSpecParams);

            var products = await _unitOfWork.CreateRepository<Product, int>().GetAllWithSpecAsync(spec);
            var mappedProduct = _mapper.Map<IEnumerable<ProductDto>>(products);

            var countspeec = new ProductWithCountSpec(productSpecParams);

            var count =await _unitOfWork.CreateRepository<Product, int>().GetCountAsync(countspeec);

            return new PaginationResponse<ProductDto>(productSpecParams.PageSize, productSpecParams.PageIndex, count, mappedProduct) ;
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
