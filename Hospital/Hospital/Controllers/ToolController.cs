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
    [Authorize(Roles = "SuperAdmin,Admin,DepartmentManager,Doctor")]
    public class ToolController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public ToolController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        int counttool = 0;
        public IActionResult Index()
        {
            List<Tool> tool = _db.Tools.ToList();
            return View(tool);
        }
        #region Create
        public async Task<IActionResult> Create()

        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tool tool)
        {
            Tool tools = await _db.Tools.FirstOrDefaultAsync();

            #region Name
            if (!ModelState.IsValid)
            {
                return View();
            }


            bool isExist = await _db.Tools.AnyAsync(x => x.Name == tool.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda alət artıq mövcuddur.");
                return View();
            }

            #endregion

            await _db.Tools.AddAsync(tool);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update

        #region Method get
        public async Task<IActionResult> Update(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            Tool tool = _db.Tools.FirstOrDefault(x => x.Id == id);
            if (tool == null)
            {
                return BadRequest();
            }
            return View(tool);
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]

        #region Method post
        public async Task<IActionResult> Update(int? id, Tool tool)
        {
            #region Method get 2
            
            if (id == null)
            {
                return NotFound();
            }
            Tool dbtool = _db.Tools.FirstOrDefault(x => x.Id == id);
            if (dbtool == null)
            {
                return BadRequest(dbtool);
            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isExist = await _db.Tools.AnyAsync(x => x.Name == tool.Name && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda Alət (və ya avadanlıq ) artıq mövcuddur.");
                return View(dbtool);
            }
            dbtool.Name = tool.Name;
            dbtool.Count = tool.Count;
            dbtool.Price = tool.Price;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        #endregion

        #endregion
        #endregion

        #region Take Tool

        public async Task<IActionResult> Take(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }
            Tool tool = await _db.Tools.Where(x => !x.IsDeactive).FirstOrDefaultAsync(x => x.Id == id);
            if (tool == null)
            {
                return BadRequest();
            }
            return View(tool);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Take(Tool tool, int id)
        {

            if (tool.Count == null)
            {
                ModelState.AddModelError("", "Neçə ədəd götürəcəyinizi daxil edin.");
                return View();
            }

            Tool dbtool = await _db.Tools.Where(x => !x.IsDeactive).FirstOrDefaultAsync(x => x.Id == id);
            if (dbtool.Count < tool.Count)
            {
                //counttool = tool1.Count - tool.Count;
                ModelState.AddModelError("Count", "Bu alətindən " + dbtool.Count + " ədəd mövcuddur. Yenidən say daxil edin.");
                return View();
            }
            dbtool.Count = dbtool.Count - tool.Count;
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
            Tool tool = await _db.Tools.FirstOrDefaultAsync(x => x.Id == id);

            if (tool == null)
            {
                return BadRequest();
            }
            if (tool.IsDeactive)
            {
                tool.IsDeactive = false;
            }
            else
            {
                tool.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        #endregion
    }
}
