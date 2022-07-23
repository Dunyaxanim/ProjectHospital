using Hospital.DAL;
using Hospital.Helpers;
using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [Authorize(Roles = "SuperAdmin,DepartmentManager,Admin")]
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public DepartmentController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        #region İndex
        public IActionResult Index()
        {
            List<Department> departments = _db.Departments.Include(x => x.HekimDepartment).ThenInclude(x => x.Hekim).ToList();
            return View(departments);
        }
        #endregion

        #region Create
        
        public async Task<IActionResult> Create()
        {
            Department department = await _db.Departments.Include(x => x.HekimDepartment).ThenInclude(x => x.Hekim).FirstOrDefaultAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            //Department department2 = await _db.Departments.Include(x => x.HekimDepartment).ThenInclude(x => x.Hekim).FirstOrDefaultAsync();

            #region Name
           
            bool isExist = await _db.Departments.AnyAsync(x => x.DepartmentName == department.DepartmentName);
            if (isExist)
            {
                ModelState.AddModelError("DepartmentName", "Bu adda şöbə artıq mövcuddur.");
                return View();
            }

            #endregion

            //#endregion

            #region Photo

            if (department.Photo == null)
            {
                ModelState.AddModelError("Photo", "Şəkil daxil edilməmişdir.");
                return View();
            }
            if (department.Photo != null)
            {
                if (!department.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Bu sənəd şəkil formatında deyil.");
                    return View();
                }
                if (department.Photo.IsElder400kb())
                {
                    ModelState.AddModelError("Photo", "400kb- dan aşağı ölçüdə şəkil yükləyin. ");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "dist", "img");
                department.Picture = await department.Photo.SaveFileAsync(folder);
            }

            #endregion

            await _db.Departments.AddAsync(department);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
        

        #region Update

        #region Method get
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Department = await _db.Departments.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            Department department = _db.Departments.FirstOrDefault(x => x.Id == id);
            if (department == null)
            {
                return BadRequest();
            }
            return View(department);
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]

        #region Method post
        public async Task<IActionResult> Update(int? id, Department department)
        {
            #region Method get 2
            ViewBag.Department = await _db.Departments.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            Department dbdepartment = await _db.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (dbdepartment == null)
            {
                return BadRequest(dbdepartment);
            }
            #endregion

            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}

            //#region Name
            //if (department.DepartmentName == null)
            //{
            //    ModelState.AddModelError("DepartmentName", "Şöbə adı daxil edilməmişdir.");
            //    return View(dbdepartment);
            //}

            bool isExist = await _db.Departments.AnyAsync(x => x.DepartmentName == department.DepartmentName && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("DepartmentName", "Bu adda şöbə artıq mövcuddur.");
                return View(dbdepartment);
            }

            //#endregion

            //#region İnfo
            //if (department.Description == null)
            //{
            //    ModelState.AddModelError("Description", "Şöbə haqqında məlumat daxil edilməmişdir.");
            //    return View(dbdepartment);
            //}
            //#endregion

            #region Photo
            if (department.Photo != null)
            {
                if (!department.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Bu sənəd şəkil formatında deyil.");
                    return View(dbdepartment);
                }
                if (department.Photo.IsElder400kb())
                {
                    ModelState.AddModelError("Photo", "400kb- dan aşağı ölçüdə şəkil yükləyin. ");
                    return View(dbdepartment);
                }
                string folder = Path.Combine(_env.WebRootPath, "dist", "img");
                dbdepartment.Picture = await department.Photo.SaveFileAsync(folder);
            }
            #endregion


            dbdepartment.DepartmentName = department.DepartmentName;
            dbdepartment.Description = department.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        #endregion

        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Department department = await _db.Departments.FirstOrDefaultAsync(x=>x.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Department department = await _db.Departments.FirstOrDefaultAsync(x => x.Id == id);

            if (department == null)
            {
                return BadRequest();
            }
            if (department.IsDeactive)
            {
                department.IsDeactive = false;
            }
            else
            {
                department.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        #endregion

    }
}
