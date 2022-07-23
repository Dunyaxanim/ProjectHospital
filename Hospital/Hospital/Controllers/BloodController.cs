using Hospital.DAL;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Hospital.Helpers.Helper;

namespace Hospital.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,DepartmentManager")]
    public class BloodController : Controller
    {
        private readonly AppDbContext _db;
        public BloodController(AppDbContext db)
        {
            _db = db;
            
        }

        #region İndex
        public IActionResult Index()
        {
            List<Blood> bloods = _db.Bloods.ToList();
            return View(bloods);
        }
        #endregion
       
        #region Update

        public async Task<IActionResult> Update(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }
            Blood blood = _db.Bloods.FirstOrDefault(x => x.Id == id);
            if (blood == null)
            {
                return BadRequest();
            }
            return View(blood);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,Blood blood)
        {
            if (id == null)
            {
                return NotFound();
            }
            Blood dbblood = _db.Bloods.FirstOrDefault(x => x.Id == id);
            if (blood == null)
            {
                return BadRequest();
            }
            
            dbblood.Quantity = blood.Quantity;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Take Tool

        public async Task<IActionResult> Take(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }
            Blood blood = await _db.Bloods.FirstOrDefaultAsync(x => x.Id == id);
            if (blood == null)
            {
                return BadRequest();
            }
            return View(blood);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Take(Blood blood, int id)
        {

            

            Blood dbblood = await _db.Bloods.FirstOrDefaultAsync(x => x.Id == id);
            if (dbblood.Quantity == 0)
            {

                ModelState.AddModelError("Quantity", "Bu qan miqdarından hal hazırda mövcud deyil.");
                return View();
            }
            if (dbblood.Quantity < blood.Quantity)
            {
               
                ModelState.AddModelError("Quantity", "Bu qan miqdarından " + dbblood.Quantity + " L mövcuddur. Yenidən say daxil edin.");
                return View();
            }
            
            dbblood.Quantity = dbblood.Quantity - blood.Quantity;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
