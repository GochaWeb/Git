using Karizi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Karizi.Controllers
{
    public class ServicesController : Controller
    {
        DB_A3C991_karizi2018Entities db = null;
        void CheckConnection()
        {
            if (db == null)
            {
                db = new DB_A3C991_karizi2018Entities();
            }
        }
        [ValidateInput(false)]
        public ActionResult Index(string by)
        {
            CheckConnection();
            ViewBag.Production = db.Productions.Where(x => x.Active == true).ToList();
            ViewBag.Services = db.Services.Where(x => x.Active == true).ToList();
            ViewBag.Portfolio = db.Portfolios.Where(x => x.Active == true).ToList();
            ViewBag.Contact = db.Contacts.Where(x => x.Id == 1).FirstOrDefault();
           

             return View(db.Services.Where(x=>x.Active==true).ToList());
            
        }
        [ValidateInput(false)]
        public ActionResult Category(string by)
        {
            CheckConnection();
            
            ViewBag.Production = db.Productions.Where(x => x.Active == true).ToList();
            ViewBag.Services = db.Services.Where(x => x.Active == true).ToList();
            ViewBag.Portfolio = db.Portfolios.Where(x => x.Active == true).ToList();
            ViewBag.Contact = db.Contacts.Where(x => x.Id == 1).FirstOrDefault();
            if (by != null && db.Services.Any(x=>x.Name==by))
            {
                var model = db.Services.Where(x => x.Name == by).FirstOrDefault().Id;
                var filter = db.ServicesPhotoes.Where(x => x.ServiceId == model).ToList();
                ViewBag.Name = db.Services.Where(x => x.Id == model).FirstOrDefault().Name;


                return View(filter);
            }
            else
            {
                return RedirectToAction("Index");
            }
           
        }
    }
}