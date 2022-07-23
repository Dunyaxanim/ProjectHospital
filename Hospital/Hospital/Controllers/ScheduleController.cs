using Hospital.DAL;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Helpers;
using static Hospital.Helpers.Helper;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Reseption")]
    public class ScheduleController : Controller
    {
        private readonly AppDbContext _db;
        public ScheduleController(AppDbContext db)
        {
            _db = db;
        }
        #region Index
        public IActionResult Index()
        {
            List<Schedule> schedules = _db.Schedules.Include(x => x.Hekim).ToList();
            return View(schedules);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.hekim = _db.Hekims.Where(x => !x.IsDeactive && x.Schedules.Count < 1).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Schedule schedule, int? hekimid, List<string> Days)
        {

            ViewBag.hekim = _db.Hekims.Where(x => !x.IsDeactive && x.Schedules.Count < 1).ToList();
            List<Schedule> schedules = _db.Schedules.Include(x => x.Hekim).ToList();
            List<string> daysofweeks = new();

            #region AD
            if (hekimid == null)
            {
                ModelState.AddModelError("", "Həkimin adını daxil edin.");
                return View();
            }
           
            #endregion

            if (Days.Count == 0)
            {
                ModelState.AddModelError("", "İşlədiyi günləri daxil edin.");
                return View();
            }
            foreach (var d in Days)
            {
                daysofweeks.Add(d);
            }
            string combinedString = string.Join(",", daysofweeks.ToArray());
            schedule.AvailableDays = combinedString;
           
            schedule.HekimId =(int) hekimid;
            await _db.Schedules.AddAsync(schedule);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.hekim = _db.Hekims.Where(x => !x.IsDeactive && x.Schedules.Count < 1).ToList();
            
            if (id == null)
            {
                return NotFound();
            }
            Schedule schedule = await _db.Schedules.FirstOrDefaultAsync(x => x.Id == id);
            if (schedule == null)
            {
                return BadRequest();
            }
            return View(schedule);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Schedule schedule, int? hekimid, List<string> Days)
        {

            ViewBag.hekim = _db.Hekims.Where(x => !x.IsDeactive && x.Schedules.Count < 1).ToList();
            

            if (id == null)
            {
                return NotFound();
            }
            Schedule bdschedule = await _db.Schedules.Include(x => x.Hekim).FirstOrDefaultAsync(x => x.Id == id);


            if (bdschedule == null)
            {
                return BadRequest();
            }
           
            bool isExist = await _db.Schedules.AnyAsync(x => x.Id == hekimid && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Hekim.Name", "Bu adda həkim artıq mövcuddur.");
                return View(bdschedule);
            }
            #region Hekim
            
            #endregion

            #region Days
            if (Days.Count==0)
            {
                ModelState.AddModelError("", "İşlədiyi günləri daxil edin.");
                return View();
            }

            List<string> daysofweeks = new();

            foreach (string d in Days)
            {
                daysofweeks.Add(d);
            }
            string combinedString = string.Join(", ", daysofweeks.ToArray());
            bdschedule.AvailableDays = combinedString;

            #endregion
            
            bdschedule.StartTime = schedule.StartTime;
            bdschedule.EndTime = schedule.EndTime;

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
            Schedule schedule = await _db.Schedules.FirstOrDefaultAsync(x => x.Id == id);

            if (schedule == null)
            {
                return BadRequest();
            }
            if (schedule.IsDeactive)
            {
                schedule.IsDeactive = false;
            }
            else
            {
                schedule.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
