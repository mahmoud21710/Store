using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.G04.APIs.Error;
using Store.G04.APIs.Excentstions;
using Store.G04.Core.Dtos.Auth;
using Store.G04.Core.Entities.Identity;
using Store.G04.Core.Services.Contract;
using System.Security.Claims;

namespace Store.G04.APIs.Controllers
{
    
    public class AccountsController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountsController(
            IUserService userService
            ,UserManager<AppUser> userManager,
            ITokenService tokenService,
            IMapper mapper
            )
        {
            _userService = userService;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        //[ProducesResponseType(typeof(Task<ActionResult<UserDto>>),StatusCodes.Status200OK)]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LogIn(LogInDto logInDto) 
        {
            var user = await _userService.LogInAsync(logInDto);
            if (user is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            return Ok(user);
        }
        //[ProducesResponseType(typeof (Task<ActionResult<UserDto>>),StatusCodes.Status200OK)]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterIn(RegisterDto registerDto) 
        {
            var user =await _userService.RegisterAsync(registerDto);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(user);
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()  //Who log in 
        {
            string userEmail = User.FindFirstValue(ClaimTypes.Email);

            if(userEmail is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var user = await _userManager.FindByEmailAsync(userEmail); 

            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(new UserDto()
            {
                Email = userEmail,
                DisplayName = user.DisplayName,    
                Token="Noooooooo"
            });
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<UserDto>> GetCurrentUserAddress()  //Who log in 
        {
            
            var user = await _userManager.FindByEmailWithAddressAsync(User);

            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(_mapper.Map<AddressDto>(user.Address));
            
        }
    }
}
