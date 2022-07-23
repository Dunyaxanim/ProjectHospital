using Hospital.DAL;
using Hospital.Helpers;
using Hospital.ViewModels;
using Hospital.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.Controllers
{
    [Authorize(Roles = "SuperAdmin,DepartmentManager,Admin")]
    public class DoctorController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public DoctorController(AppDbContext db, IWebHostEnvironment env, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _db = db;
            _env = env;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Index
        public IActionResult Index()
        {
            List<Hekim> hekims = _db.Hekims.Include(x => x.HekimDepartment).ThenInclude(x => x.Department).ToList();
            ViewBag.Department = _db.Departments.Where(x => !x.IsDeactive).ToList();
            return View(hekims);
        }
        #endregion


        #region  Create


        #region Create get
        public async Task<IActionResult> Create()
        {
            ViewBag.Department = _db.Departments.Where(x => !x.IsDeactive).ToList();
            ViewBag.Users = _db.Users./*Where(x => x.Hekims.Count < 1).*/ToList();
            return View();
        }
        #endregion
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region Create post
        public async Task<IActionResult> Create(Hekim hekim, int? departmentid)
        {
            ViewBag.Department = _db.Departments.Where(x => !x.IsDeactive).ToList();

            ViewBag.Users = _db.Users/*.Where(x => x.Hekims.Count < 1)*/.ToList();

            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}

            #region Photo
            if (hekim.Photo == null)
            {
                ModelState.AddModelError("Photo", "Şəkil daxil edilməmişdir!");
                return View();
            }
            if (hekim.Photo != null)
            {
                if (!hekim.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Bu sənəd şəkil formatında deyil.");
                    return View();
                }
                if (hekim.Photo.IsElder400kb())
                {
                    ModelState.AddModelError("Photo", "400kb aşağı ölçüdə şəkil yükləyin. ");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "dist", "img");
                hekim.Picture = await hekim.Photo.SaveFileAsync(folder);
            }
            #endregion


            bool isExist = await _db.HekimDetails.AnyAsync(x => x.PhoneNumber == hekim.HekimDetail.PhoneNumber);
            if (isExist)
            {
                ModelState.AddModelError("HekimDetail.PhoneNumber", "Bu nömrə artıq mövcuddur.");
                return View();
            }
            //if (hekim.HekimDetail.DateOfBirth > hekim.HekimDetail.Date)
            //{
            //    ModelState.AddModelError("HekimDetail.DateOfBirth", "Bu mümkün deyil.");
            //    return View();
            //}

            #region Department
            if (departmentid == null)
            {
                ModelState.AddModelError("", "Şöbə seçin.");
                return View();
            };
            HekimDepartment hekimDepartment1 = new()
            {
                DepartmentId = (int)departmentid
            };
            List<HekimDepartment> hekimDepartment = new();
            hekimDepartment.Add(hekimDepartment1);
            hekim.HekimDepartment = hekimDepartment;
            #endregion

            await _db.Hekims.AddAsync(hekim);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #endregion

        #region Update

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Department = _db.Departments.ToList();
            ViewBag.Users = _db.Users.ToList();
            if (id == null)
            {
                return NotFound();
            }
            Hekim hekim = await _db.Hekims.Include(x => x.HekimDetail).Include(x => x.HekimDepartment).ThenInclude(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
            if (hekim == null)
            {
                return BadRequest();
            }
            return View(hekim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id, Hekim hekim, int? departmentid)
        {
            ViewBag.Department = _db.Departments.Where(x=> !x.IsDeactive).ToList();
            ViewBag.Users = _db.Users.ToList();
            if (id == null)
            {
                return NotFound();
            }
            Hekim dbhekim = await _db.Hekims.Include(x => x.HekimDetail).Include(x => x.HekimDepartment).ThenInclude(x => x.Department ).FirstOrDefaultAsync(x => x.Id == id);

            if (dbhekim == null)
            {
                return BadRequest();
            }

            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}

            #region Photo
            if (hekim.Photo != null)
            {
                if (!hekim.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Şəkil daxil edin ");
                    return View(dbhekim);
                }
                if (hekim.Photo.IsElder400kb())
                {
                    ModelState.AddModelError("Photo", "Sənəd şəkil formatında deyil.");
                    return View(dbhekim);
                }
                string folder = Path.Combine(_env.WebRootPath, "dist", "img");
                dbhekim.Picture = await hekim.Photo.SaveFileAsync(folder);
            }
            #endregion
            bool isExist = await _db.HekimDetails.AnyAsync(x => x.PhoneNumber == hekim.HekimDetail.PhoneNumber && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("HekimDetail.PhoneNumber", "Bu nömrə artıq mövcuddur.");
                return View(dbhekim);
            }

            if (departmentid == null)
            {
                ModelState.AddModelError("", "Şöbə seçin.");
                return View(dbhekim);
            };
            #region Department

            dbhekim.Name = hekim.Name;
            dbhekim.SureName = hekim.SureName;
            dbhekim.HekimDetail.PhoneNumber = hekim.HekimDetail.PhoneNumber;
            dbhekim.HekimDetail.Adress = hekim.HekimDetail.Adress;
            dbhekim.HekimDetail.Destination = hekim.HekimDetail.Destination;
            dbhekim.HekimDetail.DateOfBirth = hekim.HekimDetail.DateOfBirth;
            dbhekim.HekimDetail.Date = hekim.HekimDetail.Date;
            dbhekim.HekimDetail.BloodGroup = hekim.HekimDetail.BloodGroup;

            HekimDepartment hekimDepartment1 = new()
            {
                DepartmentId = (int)departmentid
            };
            List<HekimDepartment> hekimDepartment = new();
            hekimDepartment.Add(hekimDepartment1);
            dbhekim.HekimDepartment = hekimDepartment;
            #endregion
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Hekim hekim = await _db.Hekims.FirstOrDefaultAsync(x => x.Id == id);

            if (hekim == null)
            {
                return BadRequest();
            }
            if (hekim.IsDeactive)
            {
                hekim.IsDeactive = false;
            }
            else
            {
                hekim.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        #endregion

    }
}
