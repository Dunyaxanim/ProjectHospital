using Hospital.DAL;
using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Hospital.Helpers.Helper;

namespace Hospital.Controllers
{
    [Authorize(Roles = "SuperAdmin,Accountant,Admin")]
    public class ExpenseCategoryController : Controller
    {
        private readonly AppDbContext _db;

        public ExpenseCategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<ExpenseCategory> expenseCategories = _db.ExpenseCategories.ToList();
            return View(expenseCategories);
        }
        
        #region Create
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseCategory expenseCategory)
        {
            #region Name
            if (expenseCategory.Name == null)
            {
                ModelState.AddModelError("Name", "Kateqoriya adı daxil edilməmişdir.");
                return View();
            }

            bool isExist = await _db.ExpenseCategories.AnyAsync(x => x.Name == expenseCategory.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda Kateqoriya artıq mövcuddur.");
                return View();
            }

            #endregion
            await _db.ExpenseCategories.AddAsync(expenseCategory);
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
            ExpenseCategory expense = _db.ExpenseCategories.FirstOrDefault();
            if (expense == null)
            {
                return BadRequest();
            }
            return View(expense);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, ExpenseCategory expenseCategory)
        {
            if (id == null)
            {
                return NotFound();
            }
            ExpenseCategory dbexpense = _db.ExpenseCategories.FirstOrDefault(x => x.Id == id);
            if (dbexpense == null)
            {
                return BadRequest();
            }
            #region Name
            if (expenseCategory.Name == null)
            {
                ModelState.AddModelError("Name", "Kateqoriya adı daxil edilməmişdir.");
                return View(dbexpense);
            }

            bool isExist = await _db.ExpenseCategories.AnyAsync(x => x.Name == expenseCategory.Name && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda Kateqoriya artıq mövcuddur."); 
                return View(dbexpense);
            }

            #endregion
            dbexpense.Name = expenseCategory.Name;
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
            ExpenseCategory expenseCategory = await _db.ExpenseCategories.FirstOrDefaultAsync(x => x.Id == id);

            if (expenseCategory == null)
            {
                return BadRequest();
            }
            if (expenseCategory.IsDeactive)
            {
                expenseCategory.IsDeactive = false;
            }
            else
            {
                expenseCategory.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        #endregion
    }
}
