using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;
using Store.G04.Core.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Services.Contract
{
    public interface IUserService
    {
        Task<UserDto> LogInAsync(LogInDto logInDto);
        Task<UserDto> RegisterAsync(RegisterDto registerDto);

        Task<bool> CkeckEmailExistAsync(string email);
    }
}
