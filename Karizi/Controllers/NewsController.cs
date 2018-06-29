using Karizi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Karizi.Controllers
{
    public class NewsController : Controller
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
            return View(db.News.ToList());
        }

        [ValidateInput(false)]
        public ActionResult Details([DefaultValue(0)]int by)
        {
            CheckConnection();
            ViewBag.Production = db.Productions.Where(x => x.Active == true).ToList();
            ViewBag.Services = db.Services.Where(x => x.Active == true).ToList();
            ViewBag.Portfolio = db.Portfolios.Where(x => x.Active == true).ToList();
            ViewBag.Contact = db.Contacts.Where(x => x.Id == 1).FirstOrDefault();
            if (db.News.Any(x => x.Id == by))
            {
                return View(db.News.Where(x => x.Id == by).FirstOrDefault());
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}