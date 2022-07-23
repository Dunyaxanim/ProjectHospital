using Hospital.DAL;
using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [Authorize(Roles = "SuperAdmin,Accountant,Admin")]
    public class IncomeCategoryController : Controller
    {
        private readonly AppDbContext _db;

        public IncomeCategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<IncomeCategory> incomeCategories = _db.IncomeCategories.ToList();
            return View(incomeCategories);
        }
        #region Create
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncomeCategory incomeCategory)
        {
            #region Name
            if (incomeCategory.Name == null)
            {
                ModelState.AddModelError("Name", "Kateqoriya adı daxil edilməmişdir.");
                return View();
            }

            bool isExist = await _db.IncomeCategories.AnyAsync(x => x.Name == incomeCategory.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda Kateqoriya artıq mövcuddur.");
                return View();
            }

            #endregion
            await _db.IncomeCategories.AddAsync(incomeCategory);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IncomeCategory income = _db.IncomeCategories.FirstOrDefault();
            if (income == null)
            {
                return BadRequest();
            }
            return View(income);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, IncomeCategory incomeCategory)
        {
            if (id == null)
            {
                return NotFound();
            }
            IncomeCategory dbincome = _db.IncomeCategories.FirstOrDefault(x => x.Id == id);
            if (dbincome == null)
            {
                return BadRequest();
            }
            

            #region Name
            if (incomeCategory.Name == null)
            {
                ModelState.AddModelError("Name", "Kateqoriya adı daxil edilməmişdir.");
                return View(dbincome);
            }

            bool isExist = await _db.IncomeCategories.AnyAsync(x => x.Name == incomeCategory.Name && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda Kateqoriya artıq mövcuddur.");
                return View(dbincome);
            }

            #endregion
            dbincome.Name = incomeCategory.Name;
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
            IncomeCategory incomeCategory = await _db.IncomeCategories.FirstOrDefaultAsync(x => x.Id == id);

            if (incomeCategory == null)
            {
                return BadRequest();
            }
            if (incomeCategory.IsDeactive)
            {
                incomeCategory.IsDeactive = false;
            }
            else
            {
                incomeCategory.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        #endregion
    }
}
