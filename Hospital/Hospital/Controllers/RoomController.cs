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
    [Authorize(Roles = "SuperAdmin,Admin,DepartmentManager,Reseption")]
    public class RoomController : Controller
    {
        private readonly AppDbContext _db;

        public RoomController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Room> rooms = _db.Rooms.Include(x=>x.Department).ToList();
            return View(rooms);
        }
        #region Create

        public async Task<IActionResult> Create()
        {
            ViewBag.Department = _db.Departments.Where(x=> !x.IsDeactive).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room, int departmentid)
        {
            //Room rooms = await _db.Rooms.Include(x => x.Department).FirstOrDefaultAsync();
            ViewBag.Department = _db.Departments.Where(x => !x.IsDeactive).ToList();

            #region Errors

            if (departmentid == null)
            {
                ModelState.AddModelError("", "Departament adını daxil edin.");
                return View();
            }
            bool isExist = await _db.Rooms.AnyAsync(x => x.Number == room.Number);
            if (isExist)
            {
                ModelState.AddModelError("", "Bu otaq artıq mövcuddur.");
                return View();
            }
            if (room.Number == 0)
            {
                ModelState.AddModelError("", "Otağın nömrəsini daxil edin.");
                return View();
            }
            if (room.Number == null)
            {
                ModelState.AddModelError("", "Otağın nömrəsini daxil edin.");
                return View();
            }
            if (room.Capacity == null)
            {
                ModelState.AddModelError("", "Otağın tutumunu daxil edin.");
                return View();
            }

            #endregion
            room.DepartmentId = departmentid;
            await _db.Rooms.AddAsync(room);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion

        #region Update

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Department = _db.Departments.Where(x => !x.IsDeactive).ToList();
            if (id == null)
            {
                return NotFound();
            }
            Room room = _db.Rooms.FirstOrDefault(x => x.Id == id);
            if (room == null)
            {
                return BadRequest();
            }
            return View(room);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,Room room, int departmentid)
        {
            Room rooms = await _db.Rooms.Include(x => x.Department).FirstOrDefaultAsync();
            ViewBag.Department = _db.Departments.Where(x => !x.IsDeactive).ToList();
            if (id == null)
            {
                return NotFound();
            }
            Room bdroom = await _db.Rooms.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
            if (bdroom == null)
            {
                return BadRequest();
            }
            //if (!ModelState.IsValid)
            //{
            //    return View(bdroom);
            //}
            #region Errors

            if (departmentid == null)
            {
                ModelState.AddModelError("", "Departament adını daxil edin.");
                return View(bdroom);
            }
            bool isExist = await _db.Rooms.AnyAsync(x => x.Number == room.Number && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("", "Bu otaq artıq mövcuddur.");
                return View(bdroom);
            }
            if (room.Number == 0)
            {
                ModelState.AddModelError("", "Otağın nömrəsini daxil edin.");
                return View(bdroom);
            }
            if (room.Number == null)
            {
                ModelState.AddModelError("", "Otağın nömrəsini daxil edin.");
                return View(bdroom);
            }
            if (room.Capacity == null)
            {
                ModelState.AddModelError("", "Otağın tutumunu daxil edin.");
                return View(bdroom);
            }

            #endregion
            bdroom.DepartmentId = departmentid;
            bdroom.Capacity = room.Capacity;
            bdroom.Number = room.Number;
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
            Room room = await _db.Rooms.FirstOrDefaultAsync(x => x.Id == id);

            if (room == null)
            {
                return BadRequest();
            }
            if (room.IsDeactive)
            {
                room.IsDeactive = false;
            }
            else
            {
                room.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
