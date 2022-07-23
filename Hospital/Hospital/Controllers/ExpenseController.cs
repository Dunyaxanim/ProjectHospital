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
    public class ExpenseController : Controller
    {
        private readonly AppDbContext _db;

        public ExpenseController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Expense> incomes = _db.Expenses.Include(x => x.ExpenseCategory).ToList();
            return View(incomes);
        }
        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.ExpenseCat = _db.ExpenseCategories.Where(x => !x.IsDeactive).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expense expense, int expensecatid)
        {
            ViewBag.ExpenseCat = _db.ExpenseCategories.Where(x => !x.IsDeactive).ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            expense.ExpenseCategoryId = expensecatid;
            await _db.Expenses.AddAsync(expense);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.ExpenseCat = _db.ExpenseCategories.Where(x => !x.IsDeactive).ToList();
            if (id == null)
            {
                return NotFound();
            }
            Expense expense = _db.Expenses.FirstOrDefault(x=>x.Id==id);
            if (expense == null)
            {
                return BadRequest();
            }
            return View(expense);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Expense expense, int expensecatid)
        {
            ViewBag.ExpenseCat = _db.ExpenseCategories.Where(x => !x.IsDeactive).ToList();
            if (id == null)
            {
                return NotFound();
            }
            Expense dbexpense = _db.Expenses.FirstOrDefault(x => x.Id == id);
            if (dbexpense == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            

            dbexpense.ExpenseCategoryId = expensecatid;
            dbexpense.Name = expense.Name;
            dbexpense.Discription = expense.Discription;
            dbexpense.Amount = expense.Amount;
            dbexpense.Date = expense.Date;
           
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
            Expense expense = await _db.Expenses.FirstOrDefaultAsync(x => x.Id == id);

            if (expense == null)
            {
                return BadRequest();
            }
            if (expense.IsDeactive)
            {
                expense.IsDeactive = false;
            }
            else
            {
                expense.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        #endregion
    }
}
