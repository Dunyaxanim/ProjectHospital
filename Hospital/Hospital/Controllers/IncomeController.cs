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
    
    public class IncomeController : Controller
    {
        private readonly AppDbContext _db;

        public IncomeController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Income> incomes = _db.Incomes.Include(x => x.IncomeCategory).ToList();
            return View(incomes);
        }
        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.IncomeCat = _db.IncomeCategories.Where(x => !x.IsDeactive).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Income income, int incomecatid)
        {

            ViewBag.IncomeCat = _db.IncomeCategories.Where(x => !x.IsDeactive).ToList();
            income.IncomeCategoryId = incomecatid;
            await _db.Incomes.AddAsync(income);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.IncomeCat = _db.IncomeCategories.Where(x => !x.IsDeactive).ToList();
            if (id == null)
            {
                return NotFound();
            }
            Income income = _db.Incomes.FirstOrDefault(x => x.Id == id);
            if (income == null)
            {
                return BadRequest();
            }
            return View(income);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Income income, int incomecatid)
        {
            ViewBag.IncomeCat = _db.IncomeCategories.Where(x => !x.IsDeactive).ToList();
            if (id == null)
            {
                return NotFound();
            }
            Income dbincome = _db.Incomes.FirstOrDefault(x => x.Id == id);
            if (dbincome == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            dbincome.IncomeCategoryId = incomecatid;
            dbincome.Name = income.Name;
            dbincome.Discription = income.Discription;
            dbincome.Amount = income.Amount;
            dbincome.Date = income.Date;

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
            Income income = await _db.Incomes.FirstOrDefaultAsync(x => x.Id == id);

            if (income == null)
            {
                return BadRequest();
            }
            if (income.IsDeactive)
            {
                income.IsDeactive = false;
            }
            else
            {
                income.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
