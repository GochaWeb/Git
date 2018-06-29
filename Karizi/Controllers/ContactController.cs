using Karizi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Karizi.Controllers
{
    public class ContactController : Controller
    {
        DB_A3C991_karizi2018Entities db = null;
        void CheckConnection()
        {
            if (db == null)
            {
                db = new DB_A3C991_karizi2018Entities();
            }
        }
        public ActionResult Index()
        {
            CheckConnection();
            ViewBag.Production = db.Productions.Where(x => x.Active == true).ToList();
            ViewBag.Services = db.Services.Where(x => x.Active == true).ToList();
            ViewBag.Portfolio = db.Portfolios.Where(x => x.Active == true).ToList();
            ViewBag.Contact = db.Contacts.Where(x => x.Id == 1).FirstOrDefault();
            return View();
        }

        public ActionResult Message(MessageModel model)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("karizimessage@gmail.com");
            mail.To.Add(new MailAddress("karizikarizi@mail.ru"));
            mail.Subject = model.Name;

            mail.Body = "მომხმარებლის ელ-ფოსტა :" + model.Email + "<br />" + "ტექსტი : " + model.Text;

            mail.IsBodyHtml = true;





            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            System.Net.NetworkCredential credential = new System.Net.NetworkCredential();

            credential.UserName = "karizimessage@gmail.com";
            credential.Password = "karizi2018";

            smtp.Credentials = credential;
            smtp.EnableSsl = true;
            smtp.Send(mail);

            TempData["Success"] = "მესიჯი წარმატებით გაიგზავნა";
            return RedirectToAction("Index");
        }
    }
}