using Bai5_63135741.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Bai5_63135741.Controllers
{
    public class SendMail_63135741Controller : Controller
    {
        // GET: SendMail_63135741
        public ActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendMail(MailInfo model)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(model.From);
            mail.To.Add(model.To);
            mail.Subject = model.Subject;
            mail.Body = model.Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential(model.From, model.Password);
            smtp.EnableSsl = true;
            smtp.Send(mail);

            return RedirectToAction("Index");
        }
    }
}