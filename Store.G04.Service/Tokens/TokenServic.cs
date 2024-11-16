using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Store.G04.Core.Entities.Identity;
using Store.G04.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Store.G04.Service.Tokens
{
    public class TokenServic : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenServic(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       
        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {
            var claimUser = new List<Claim>()
            {
                 new Claim(ClaimTypes.Name ,user.UserName ) ,
                 new Claim(ClaimTypes.GivenName ,user.DisplayName ) ,
                 new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
                 new Claim (ClaimTypes.Email ,user.Email)
            };

            var userRoles =await userManager.GetRolesAsync(user);
            foreach (var role in userRoles) 
            {
                claimUser.Add(new Claim(ClaimTypes.Role, role));
            }
            var secKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:DurationInDays"])),
                claims: claimUser,
                signingCredentials: new SigningCredentials(secKey,SecurityAlgorithms.HmacSha256Signature)
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
