using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.APIs.Error;
using Store.G04.Core;
using Store.G04.Core.Dtos.Orders;
using Store.G04.Core.Entities.OrderEntities;
using Store.G04.Core.Services.Contract;
using System.Security.Claims;

namespace Store.G04.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IOrderService orderService , IMapper mapper,IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderDto model)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null) return Unauthorized (new ApiErrorResponse(StatusCodes.Status401Unauthorized));

            var address = _mapper.Map<Address>(model.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(userEmail, model.BasketId, model.DeliveryMethodId, address);

            if(order is  null) return BadRequest (new ApiErrorResponse (StatusCodes.Status400BadRequest));

            return Ok (_mapper.Map<OrdertoReturnDto>(order));
        }


        [Authorize]
        [HttpGet("getorderfospecificuser")]
        public async Task<IActionResult> GetOrdersForSpecificUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

            var orders = await _orderService.GetOrdersForSpecificUserAsync(userEmail);

            if (orders is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest)); 

            return Ok(_mapper.Map<IEnumerable<OrdertoReturnDto>>(orders));
        }
        [Authorize]
        [HttpGet("getorderfospecificuserbyid/{orderId}")]
        public async Task<IActionResult> GetOrderForSpecificUser(int orderId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

            var order = await _orderService.GetOrdersByIdForSpecificUserAsync(userEmail, orderId);

            if (order is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return Ok(_mapper.Map<OrdertoReturnDto>(order));
        }

        [HttpGet("DeliveryMethods")]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            var deliveryMethod =await  _unitOfWork.CreateRepository<DeliveryMethod, int>().GetAllAsync();

            if (deliveryMethod is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(deliveryMethod);
        }
    }
}
