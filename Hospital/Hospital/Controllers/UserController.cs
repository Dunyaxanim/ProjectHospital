using Hospital.DAL;
using Hospital.Models;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Hospital.Helpers.Helper;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Mail;

namespace Hospital.Areas.Admin.Controllers
{

    [Authorize(Roles = "SuperAdmin,Admin")]
    public class UserController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        public UserController(AppDbContext db, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        #region View Users
        public async Task<IActionResult> Index()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            List<UserVM> userVMs = new List<UserVM>();
            List<IdentityRole> role = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = role;
            foreach (AppUser user in users)
            {
                UserVM userVM = new UserVM()
                {
                    Id = user.Id,
                    Fullname = user.Name + " " + user.Surname,
                    Username = user.UserName,
                    Email = user.Email,
                    IsDeactive = user.IsDeactive,
                    Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
                };
                userVMs.Add(userVM);
            }
            return View(userVMs);
        }
        #endregion

        #region Change Role

        public async Task<IActionResult> ChangeRole(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }

            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = roles;
            ChangeRoleVM changeRoleVM = new()
            {
                OldRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
            };
            if (changeRoleVM.OldRole == null)
            {
                return BadRequest();
            }
            return View(changeRoleVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(string id, /*ChangeRoleVM changeRole,*/ string NewRole)
        {

            if (id == null)
            {
                return NotFound();
            }
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = roles;
            ChangeRoleVM changeRoleVM = new()
            {
                OldRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
            };
            if (changeRoleVM.OldRole == null)
            {
                return BadRequest();
            }
            if (NewRole != changeRoleVM.OldRole)
            {
                if (roles.Exists(x => x.Name != NewRole))
                {
                    ModelState.AddModelError("", "Bele rol movcud deyil");
                }
                IdentityResult addidentityResult = await _userManager.AddToRoleAsync(user, NewRole);
                if (!addidentityResult.Succeeded)
                {
                    foreach (IdentityError error in addidentityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(changeRoleVM);
                }
                IdentityResult removeidentityResult = await _userManager.RemoveFromRoleAsync(user, changeRoleVM.OldRole);
                if (!removeidentityResult.Succeeded)
                {
                    foreach (IdentityError error in removeidentityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(changeRoleVM);
                }
            }
            return RedirectToAction("Index");

        }
        #endregion

        #region EditUser
        public async Task<IActionResult> UpdateUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            UpdateUser updateUser = new UpdateUser()
            {
               
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName,
                Email = user.Email
               
            };
            return View(updateUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(string id,UpdateUser newupdateUser)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            UpdateUser updateUserdb = new UpdateUser()
            {

                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName,
                Email = user.Email

            };
            if (!ModelState.IsValid)
            {
                return View(updateUserdb);
            }
            user.Email = newupdateUser.Email;
            user.UserName = newupdateUser.UserName;
            user.Name = newupdateUser.Name;
            user.Surname = newupdateUser.Surname;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
        #endregion

        #region Create User
        public async Task<IActionResult> CreateUser()
        {
            List<IdentityRole> role = await _roleManager.Roles.Where(x=>x.Name !="SuperAdmin").ToListAsync();
            ViewBag.Roles = role;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateUser(CreateVM create, string sellectedrole)
        {
            List<IdentityRole> role = await _roleManager.Roles.Where(x => x.Name != "SuperAdmin").ToListAsync();
            ViewBag.Roles = role;
            
            AppUser appuserEmail = await _userManager.FindByEmailAsync(create.Email);
            AppUser appuserUserName = await _userManager.FindByNameAsync(create.UserName);
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (appuserEmail !=null)
            {
                ModelState.AddModelError("","Bu hesab artıq mövcuddur.");
            }

            if (appuserUserName != null)
            {
                ModelState.AddModelError("", "Bu adda hesab artıq mövcuddur.");
            }
            //if (role. != "SuperAdmin")
            //{
            //    ModelState.AddModelError("", "Bu adda hesab artıq mövcuddur.");
            //}

            AppUser appUser = new()
            {
                Name = create.Name,
                Surname = create.SureName,
                UserName = create.UserName,
                Email = create.Email
            };
            IdentityResult identityResult = await _userManager.CreateAsync(appUser, create.Password);
            if (!identityResult.Succeeded)
            {
                foreach (IdentityError identityError in identityResult.Errors)
                {
                    ModelState.AddModelError("", identityError.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(appUser, sellectedrole);
            return RedirectToAction("Index", "User");
        }
        #endregion

        #region ShowRole

        public async Task<IActionResult> ShowRole(int id)
        {
            List<IdentityRole> role = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = role;
            return View(role);
        }
        #region Natamam

        //public async Task<IActionResult> ShowUserOfRole(string id)
        //{
        //    //IdentityRole role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
        //    //if (role == null)
        //    //{
        //    //    return NotFound();
        //    //}
        //    //AppUser user = await _userManager.FindByIdAsync(id);
        //    //if (user == null)
        //    //{
        //    //    return BadRequest();
        //    //}
        //    //List<AppUser> appuser = _userManager.GetUsersInRoleAsync(appuser)).FirstOrDefault()
        //    //if (role.Id == user.get)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //return View(role);
        //}

        #endregion

        private void FirstOrDefaultAsync()
        {
            throw new NotImplementedException();
        }


        #endregion

        #region UpdateRole
        //public async Task<IActionResult> UpdateRole()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UpdateRole(NewRole newRole)
        //{

        //    if (!(await _roleManager.RoleExistsAsync(newRole.Name)))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = newRole.Name });
        //    }
        //    return RedirectToAction("Index");
        //}

        #endregion

        #region Delete

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AppUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return BadRequest();
            }
            if (user.IsDeactive)
            {
                user.IsDeactive = false;
            }
            else
            {
                user.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        #endregion

        #region ForgotPassword


        //public IActionResult ForgotP()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> ForgotPAsync(ForgotPasswordVM forgotPasswordVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        AppUser user = await _userManager.FindByEmailAsync(forgotPasswordVM.Email);
        //        if (user != null)
        //        {
        //            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //            var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = forgotPasswordVM.Email, token = token }, Request.Scheme);
        //            MailMessage message = new MailMessage("dunyaxanim.y@itbrains.az", mailTo);
        //            message.Subject = messageSubject;
        //            message.Body = messageBody;
        //            message.BodyEncoding = System.Text.Encoding.UTF8; 
        //            message.IsBodyHtml = true;



        //        }
        //    }
        //    return View();
        //}
        #endregion

        #region ResetPassword

        public async Task<IActionResult> ResetPassword(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string id,ResetPasswordVM resetPasswordVM)
        {
            if (id == null)
            {
                return NotFound();
            }
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            IdentityResult identityResult = await _userManager.ResetPasswordAsync(user, token, resetPasswordVM.Password);
            if (!identityResult.Succeeded)
            {
                ModelState.AddModelError("","Şifrə minimum 6 simvoldan ibarət olmalıdır.");
                ModelState.AddModelError("","Şifrədə ən azı 1 ədəd kiçik hərf olmalıdır.");
                ModelState.AddModelError("", "Şifrədə ən azı 1 ədəd böyük hərf olmalıdır.");
                ModelState.AddModelError("", "Şifrədə ən azı 1 ədəd rəqəm olmalıdır.");
                ModelState.AddModelError("", "Şifrədə ən azı 1 ədəd simvol olmalıdır.");
            }
            return RedirectToAction("Index");

        }


        #endregion
    }
}
