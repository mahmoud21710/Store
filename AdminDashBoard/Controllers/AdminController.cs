using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Core.Dtos.Auth;
using Store.G04.Core.Entities.Identity;

namespace AdminDashBoard.Controllers
{
    public class AdminController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInDto model) 
        {
            var user =await _userManager.FindByEmailAsync(model.Email);

            if (user == null) 
            {
                ModelState.AddModelError("Email", "Invalid Email");
                return RedirectToAction(nameof(LogIn));
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded || !await _userManager.IsInRoleAsync(user, "Admin"))
            {
                ModelState.AddModelError(string.Empty, "You Are No Authorized");
                return RedirectToAction(nameof(LogIn));
            }
            else 
            {
                return RedirectToAction("Index","Home");
            }
        }

        public IActionResult LogOut() 
        {
            return RedirectToAction(nameof(LogIn));
        }
    }
}
