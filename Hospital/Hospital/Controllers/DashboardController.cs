using Hospital.DAL;
using Hospital.Models;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static Hospital.Helpers.Helper;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.Controllers
{
    
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;

        public DashboardController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            DashboardVM dashboardVM = new()
            {
                Expenses = _db.Expenses.ToList(),
                Incomes = _db.Incomes.ToList(),
                Hekims = _db.Hekims.ToList(),
                Patients = _db.Patients.ToList(),
                Users = _db.Users.ToList(),
            };
            return View(dashboardVM);
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
