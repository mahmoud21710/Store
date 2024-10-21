using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Core.Services.Contract;

namespace Store.G04.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        //EndPOint 
        [HttpGet] //BaseUrl/api/Products
        public async Task<IActionResult> GetAllProduct([FromQuery]string? sort, [FromQuery] int?brandId,[FromQuery]int? typeId, [FromQuery] int?pageSize=5 ,[FromQuery]int? pageIndex=1) 
        {
            var result = await _productService.GetAllProductsAsync(sort,brandId,typeId,pageSize,pageIndex);

            return Ok(result); //200
        }
        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _productService.GetAllBrandsAsync();
            return Ok(result);
        }
        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _productService.GetAllTypesAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsById(int? id)
        {
            if (id is null) return BadRequest("Invalid id !");
            var result = await _productService.GetProductByIdAsync(id.Value);
            if(result is null) return NotFound();
            return Ok(result);
        }

    }
}
