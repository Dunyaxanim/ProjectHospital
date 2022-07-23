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
    public class PatientController : Controller
    {
        private readonly AppDbContext _db;
       
        public PatientController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Patient> patients = _db.Patients.Include(x=>x.PatientDetail).ToList();
            return View(patients);
        }

        #region Create
        public async Task<IActionResult> Create()
        {
            Patient patients = _db.Patients.Include(x => x.PatientDetail).FirstOrDefault();
            ViewBag.Hekim = _db.Hekims.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Patient patient,int hekimid)
        {
            Patient patients = _db.Patients.Include(x => x.PatientDetail).FirstOrDefault();
            ViewBag.Hekim = _db.Hekims.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (patient.FirstName == null)
            {
                ModelState.AddModelError("", "Xəstənin adını axil edin.");
            }
            if (patient.LastName == null)
            {
                ModelState.AddModelError("", "Xəstənin soyadını axil edin.");
            }
            if (patient.PatientDetail.Adress == null)
            {
                ModelState.AddModelError("PatientDetail.Adress", "Adres axil edin.");
            }
            patient.HekimId = hekimid;
            await _db.Patients.AddAsync(patient);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Hekim = _db.Hekims.ToList();
            if (id == null)
            {
                return NotFound();
            }
            Patient patient = _db.Patients.Include(x=>x.PatientDetail).Include(x => x.Hekim).FirstOrDefault(x => x.Id == id);
            if (patient == null)
            {
                return BadRequest();
            }
            return View(patient);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]

        
        public async Task<IActionResult> Update(int id,Patient patient, int? hekimid)
        {

            ViewBag.Hekim = _db.Hekims.ToList();
            if (id == null)
            {
                return NotFound();
            }
            Patient dbpatient = _db.Patients.Include(x => x.PatientDetail).Include(x => x.Hekim).FirstOrDefault(x => x.Id == id);
            if (dbpatient == null)
            {
                return BadRequest(dbpatient);
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (patient.PatientDetail.Adress == null)
            {
                ModelState.AddModelError("PatientDetail.Adress", "Adres axil edin.");
            }
            if (patient.FirstName == null)
            {
                ModelState.AddModelError("FirstName", "Xəstənin adını axil edin.");
            }
            if (patient.LastName == null)
            {
                ModelState.AddModelError("LastName", "Xəstənin soyadını axil edin.");
            }
            bool isExist = await _db.Patients.AnyAsync(x => x.PatientDetail.HomeNumber == patient.PatientDetail.HomeNumber && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("HekimDetail.PhoneNumber", "Bu nömrə artıq mövcuddur.");
                return View(dbpatient);
            }
            dbpatient.FirstName = patient.FirstName;
            dbpatient.LastName = patient.LastName;
            dbpatient.PatientDetail.Adress = patient.PatientDetail.Adress;
            dbpatient.PatientDetail.HomeNumber = patient.PatientDetail.HomeNumber;
            dbpatient.PatientDetail.MobileNumber = patient.PatientDetail.MobileNumber;
            dbpatient.PatientDetail.EmailAdress = patient.PatientDetail.EmailAdress;
            dbpatient.PatientDetail.Adress = patient.PatientDetail.Adress;
            dbpatient.PatientDetail.Destination = patient.PatientDetail.Destination;
            dbpatient.PatientDetail.DateOfBrith = patient.PatientDetail.DateOfBrith;
            dbpatient.PatientDetail.CodeOfIdentityCard = patient.PatientDetail.CodeOfIdentityCard;
            dbpatient.PatientDetail.FatherName = patient.PatientDetail.FatherName;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Patient patient = await _db.Patients.Include(x => x.PatientDetail).FirstOrDefaultAsync(x => x.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }
        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Patient patient = await _db.Patients.FirstOrDefaultAsync(x => x.Id == id);

            if (patient == null)
            {
                return BadRequest();
            }
            if (patient.IsDeactive)
            {
                patient.IsDeactive = false;
            }
            else
            {
                patient.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        #endregion
    }
}
