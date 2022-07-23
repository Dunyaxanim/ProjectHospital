using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Helpers
{
    public static class Helper
    {
        public static async Task SendMessage(string messageSubject, string messageBody, string mailTo,IFormFile Attachment)
        {

            SmtpClient client = new SmtpClient("smtp.yandex.com", 587);
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("dunyaxanim.y@itbrains.az", "lfvjoqpcsxgmxfcp");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage message = new MailMessage("dunyaxanim.y@itbrains.az", mailTo);
            message.Subject = messageSubject;
            message.Body = messageBody;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Attachments.Add(new Attachment(Attachment.OpenReadStream(), Attachment.FileName));
            await client.SendMailAsync(message);
        }
        

        public enum Roles
        {
            SuperAdmin,
            Admin,
            Reseption,
            Doctor,
            Accountant,
            DepartmentManager
        }
        public enum ItemDay
        {
            Bazar_ertəsi,
            Çərşənbə_axşamı,
            Çərşənbə,
            Cümə_axşamı,
            Cümə,
            Şənbə,
            Bazar
        }
        //public static async Task ForgotPassword()
        //{

        //    SmtpClient client = new SmtpClient("smtp.yandex.com", 465);
        //    client.UseDefaultCredentials = false;
        //    client.EnableSsl = true;
        //    client.Credentials = new NetworkCredential("dunyaxanim.y@itbrains.az", "lfvjoqpcsxgmxfcp");
        //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    AuthenticationManager.Authenticate()
        //    (EmailTokenProvider, pass);
        //    MailMessage message = new MailMessage("dunyaxanim.y@itbrains.az", mailTo);
        //    message.Subject = messageSubject;
        //    message.Body = messageBody;
        //    message.BodyEncoding = System.Text.Encoding.UTF8;
        //    message.IsBodyHtml = true;

        //    message.Attachments.Add(new Attachment(Attachment.OpenReadStream(), Attachment.FileName));
        //    await client.SendMailAsync(message);
        //}
    }
}
