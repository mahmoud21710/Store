using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.APIs.Attributes;
using Store.G04.APIs.Error;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Entities;
using Store.G04.Core.Helper;
using Store.G04.Core.Services.Contract;
using Store.G04.Core.Specifications.Products;

namespace Store.G04.APIs.Controllers
{
   
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [ProducesResponseType(typeof(PaginationResponse<ProductDto>),StatusCodes.Status200OK)]
        [HttpGet] //BaseUrl/api/Products
        [Cached(100)]

        //[FromQuery] string? sort, [FromQuery] int? brandId,[FromQuery]int? typeId, [FromQuery] int? pageSize = 5,[FromQuery]int? pageIndex = 1
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProduct([FromQuery] ProductSpecParams productSpecParams) 
        {
            var result = await _productService.GetAllProductsAsync(productSpecParams);

            return Ok(result); //200
        }
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrands()
        {
            var result = await _productService.GetAllBrandsAsync();
            return Ok(result);
        }
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllTypes()
        {
            var result = await _productService.GetAllTypesAsync();
            return Ok(result);
        }


        [ProducesResponseType(typeof(TypeBrandDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsById(int? id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(400));
            var result = await _productService.GetProductByIdAsync(id.Value);
            if(result is null) return NotFound(new ApiErrorResponse(400,"not found"));
            return Ok(result);
        }

    }
}
