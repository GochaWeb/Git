using Karizi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Karizi.Controllers
{
    public class AboutController : Controller
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
            ViewBag.About = db.Abouts.Where(x => x.Id == 1).FirstOrDefault();
            ViewBag.Contact = db.Contacts.Where(x => x.Id == 1).FirstOrDefault();
            return View(db.AboutUsPhotoes.ToList());
        }
    }
}