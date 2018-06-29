using Karizi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Karizi.Controllers
{
    public class PortfolioController : Controller
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
            if (by != null && db.Portfolios.Any(x=>x.Name==by))
            {
                var model = db.Portfolios.Where(x => x.Active == true).ToList();
                var filter = model.Where(x => x.Name.Contains(by)).ToList();
                return View(filter);
            }
            else
            {

                return View(db.Portfolios.Where(x => x.Active == true).ToList());
            }
            
        }
        [ValidateInput(false)]
        public ActionResult Category(string by)
        {
            CheckConnection();

            ViewBag.Production = db.Productions.Where(x => x.Active == true).ToList();
            ViewBag.Services = db.Services.Where(x => x.Active == true).ToList();
            ViewBag.Portfolio = db.Portfolios.Where(x => x.Active == true).ToList();
            ViewBag.Contact = db.Contacts.Where(x => x.Id == 1).FirstOrDefault();
            if (by != null && db.Portfolios.Any(x=>x.Name==by))
            {
                var model = db.Portfolios.Where(x => x.Name == by).FirstOrDefault().Id;
                var filter = db.PortfolioPhotoes.Where(x => x.PortfolioId == model).ToList();
                ViewBag.Name = db.Portfolios.Where(x => x.Id == model).FirstOrDefault().Name;


                return View(filter);
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
    }
}