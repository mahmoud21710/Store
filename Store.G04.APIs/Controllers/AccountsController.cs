﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.APIs.Error;
using Store.G04.Core.Dtos.Auth;
using Store.G04.Core.Services.Contract;

namespace Store.G04.APIs.Controllers
{
    
    public class AccountsController : BaseApiController
    {
        private readonly IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }
        [ProducesResponseType(typeof(Task<ActionResult<UserDto>>),StatusCodes.Status200OK)]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LogIn(LogInDto logInDto) 
        {
            var user = await _userService.LogInAsync(logInDto);
            if (user is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            return Ok(user);
        }
        [ProducesResponseType(typeof (Task<ActionResult<UserDto>>),StatusCodes.Status200OK)]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterIn(RegisterDto registerDto) 
        {
            var user =await _userService.RegisterAsync(registerDto);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(user);
        }
    }
}
