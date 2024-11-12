using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.APIs.Error;
using Store.G04.Core.Dtos.Basket;
using Store.G04.Core.Entities;
using Store.G04.Core.Services.Contract;

namespace Store.G04.APIs.Controllers
{
    
    public class BasketController : BaseApiController
    {
        private readonly IBasketService _basket;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basket , IMapper mapper)
        {
            _basket = basket;
            _mapper = mapper;
        }
        
        [HttpGet("basket")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string? id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(400, "Invalid ID"));
            var Basket = await _basket.GetBsketByIdAsync(id); 
            if (Basket is null) new CustomerBasket() { Id = id };
            return Ok(Basket);
        }

        [HttpPost("createupdate")]
        public async Task<ActionResult<CustomerBasket>> CreateUpdateBasket(CustomerBasketDto model) 
        {
            var basket =  _mapper.Map<CustomerBasket>(model);

            var basket2 = await _basket.UpdateCreateBasketAsync(basket);

            if (basket is null) return BadRequest(new ApiErrorResponse(400));
            
            return  Ok(basket2);
        }

        [HttpDelete("delete")]
        public async Task DeleteBasket (string id) 
        {
             await _basket.DeleteBasket(id);
        }

    }
}
