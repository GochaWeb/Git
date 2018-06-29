using Karizi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Karizi.Controllers
{
    public class KariziCmsLoginController : Controller
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
           
            
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email,string Password)
        {
            CheckConnection();
            Login US = db.Logins.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
            if (US == null)
            {
                TempData["Error"] = "ასეთი მომხმარებელი არ არსებობს";
                return RedirectToAction("Index");
            }
            else
            {
                Session["Login"] = US;
                return RedirectToAction("Main", "KariziCms");
            }
           
        }
    }
}