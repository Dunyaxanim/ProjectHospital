using Hospital.DAL;
using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [Authorize(Roles = "SuperAdmin,DepartmentManager,Admin")]
    public class DoctorDetailController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public DoctorDetailController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index(int id)
        {
            HekimDetail hekimDetails = _db.HekimDetails.Include(x=>x.Hekim).FirstOrDefault(x=>x.Id==id);
            ViewBag.Availableday = _db.Schedules.ToList();
            return View(hekimDetails);
        }
        
    }
}
