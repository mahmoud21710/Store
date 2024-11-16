using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.G04.Core.Entities.Identity;
using System.Security.Claims;

namespace Store.G04.APIs.Excentstions
{
    public static class UserManagerExcentstions
    {
        public static async Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal user) 
        {
            var userEmail = user.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null) return null;

            var user1 = await userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == userEmail);

            if (user1 is null) return null;

            return user1;
        }
    }
}
