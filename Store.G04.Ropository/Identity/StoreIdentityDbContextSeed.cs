using Microsoft.AspNetCore.Identity;
using Store.G04.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Ropository.Identity
{
    public static class StoreIdentityDbContextSeed
    {
        public static async Task SeedAppUserAsync(UserManager<AppUser> _userManger) 
        {
            if (_userManger.Users.Count() == 0) 
            {
                var user = new AppUser()
                {
                    Email = "mahmoudnouman926@gmail.com",
                    DisplayName = "mahmoud",
                    UserName = "mahmoud.nouman",
                    PhoneNumber = "04574545785",
                    Address = new Address()
                    {
                        FName = "mahmoud",
                        LName = "numan",
                        City = "nawa",
                        Country = "egypt",
                        Street = "ter3a"
                    }
                };
                await _userManger.CreateAsync(user, "Asdf@123");
            }
        }
    }
}
