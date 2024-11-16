using Microsoft.AspNetCore.Identity;
using Store.G04.Core.Dtos.Auth;
using Store.G04.Core.Entities.Identity;
using Store.G04.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Service.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<AppUser> userManager 
                            ,SignInManager<AppUser> signInManager 
                            ,ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        

        public async Task<UserDto> LogInAsync(LogInDto logInDto)
        {
            var user = await _userManager.FindByEmailAsync(logInDto.Email);
            if (user is null) return null;
            var result =await _signInManager.CheckPasswordSignInAsync(user,logInDto.Password,false);

            if (!result.Succeeded)  return null;

            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token =await _tokenService.CreateTokenAsync(user, _userManager)
            };
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            if (await CkeckEmailExistAsync(registerDto.Email)) return null;
            var user = new AppUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DiplayName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split('@')[0]
            };
            var result = await _userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded) return null;
            return new UserDto()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DiplayName,
                Token =await _tokenService.CreateTokenAsync(user, _userManager)
            };
        }
        public async Task<bool> CkeckEmailExistAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

    }
}
