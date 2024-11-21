﻿using AdminDashBoard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.G04.Core.Entities.Identity;

namespace AdminDashBoard.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var users =await _userManager.Users.Select( U=>new UserViewModel()
            {
                Id = U.Id,
                DisplayName = U.DisplayName,
                UserName = U.UserName,
                Email = U.Email,
                Roles =  _userManager.GetRolesAsync(U).Result
            }).ToListAsync();
            return View(users);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var Allroles = await _roleManager.Roles.ToListAsync();

            var viewModel = new UserRoleViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = Allroles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    Name = R.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, R.Name).Result,
                }).ToList(),
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            var UserRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles) 
            {
                if(UserRoles.Any(r => r == role.Name && !role.IsSelected)) 
                {
                   await _userManager.RemoveFromRoleAsync(user,role.Name);
                }
                if (!UserRoles.Any(r => r == role.Name) && role.IsSelected) 
                {
                   await _userManager.AddToRoleAsync(user,role.Name);
                }
            }
            return RedirectToAction("Index");
        }
    }
}