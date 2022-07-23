using Hospital.DAL;
using Hospital.Helpers;
using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using QuickMailer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using static Hospital.Helpers.Helper;

namespace Hospital.Controllers
{
    [Authorize(Roles = "SuperAdmin,Accountant,Admin,DepartmentManager,Doctor,Reseption")]
    public class MailSSendController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public MailSSendController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region Send
        public IActionResult Send()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendAsync(EmailMessage emailMessage,IFormFile Attachment)
        {
            string mgs = "Email göndərilmədi.";
            try
            {
                await Helper.SendMessage(emailMessage.Subject, emailMessage.Body, emailMessage.To, Attachment);
                mgs = "Email göndərildi.";
            }
            catch (Exception e)
            {
                mgs = e.Message;
            }
            ViewBag.Mgs = mgs;
            return View();
        }
        #endregion

    }
}
