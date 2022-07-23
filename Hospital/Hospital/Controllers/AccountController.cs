using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.DAL;
using Hospital.Helpers;
using Hospital.Models;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(AppDbContext db, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        #region Create Admin
        public async Task<IActionResult> CreateAdmin()
        {
            await CreateRoleAsync();
            AppUser appUser = new()
            {
                Name = "Parker",
                Surname = "Feyzullayev",
                UserName = "Parker",
                Email = "Parker@hospital.com"
            };
            AppUser user = await _userManager.FindByEmailAsync(appUser.Email);
            if (user == null)
            {
                await _userManager.CreateAsync(appUser, "Parker135.");
                await _userManager.AddToRoleAsync(appUser, "SuperAdmin");
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction("Login");

        }

        public async Task<IActionResult> Register(RegisterVM register)
        {
            
            return RedirectToAction("Index", "Dashboard");

            
        }
        #endregion

        #region LogOut
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("LogIn", "Account");
        }
        #endregion

        #region Login
        public async Task<IActionResult> LogIn(LoginVM login)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = await _userManager.FindByEmailAsync(login.Email);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Belə hesab mövcud deyil.");
                return View();
            }
            if (appUser.IsDeactive)
            {
                ModelState.AddModelError("", "Siz bloklanmisiniz");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, login.Password, true, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Siz bir dəqiqəlik bloklanmısınız.");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Sizin şifrəniz yanlışdır");
                return View();
            }
            return RedirectToAction("Index", "Dashboard");
        }
        #endregion


        #region CreateRoleAsync
        public async Task CreateRoleAsync()

        {
            if (!(await _roleManager.RoleExistsAsync("SuperAdmin")))
            {
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            if (!(await _roleManager.RoleExistsAsync("Admin")))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!(await _roleManager.RoleExistsAsync("Reseption")))
            {
                await _roleManager.CreateAsync(new IdentityRole("Reseption"));
            }
            if (!(await _roleManager.RoleExistsAsync("Doctor")))
            {
                await _roleManager.CreateAsync(new IdentityRole("Doctor"));
            }
            if (!(await _roleManager.RoleExistsAsync("Accountant")))
            {
                await _roleManager.CreateAsync(new IdentityRole("Accountant"));
            }
            if (!(await _roleManager.RoleExistsAsync("DepartmentManager")))
            {
                await _roleManager.CreateAsync(new IdentityRole("DepartmentManager"));
            }

        }
        #endregion


    }
}