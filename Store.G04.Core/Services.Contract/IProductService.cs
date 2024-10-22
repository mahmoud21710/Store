﻿using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Entities;
using Store.G04.Core.Helper;
using Store.G04.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Services.Contract
{
    public interface IProductService
    {
        Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecParams productSpecParams );
        Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync(); 
        Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync(); 
        Task<ProductDto> GetProductByIdAsync(int id);


    }
}
