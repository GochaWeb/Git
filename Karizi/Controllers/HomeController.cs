using Karizi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Karizi.Controllers
{
    public class HomeController : Controller
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

            ViewBag.Count = db.SliderPhotoes.Count();
            ViewBag.Contact = db.Contacts.Where(x => x.Id == 1).FirstOrDefault();

            ViewBag.Mid = db.MiddleBlockTexts.Where(x => x.Id == 1).FirstOrDefault();
            ViewBag.Bot = db.BottomblockTexts.Where(x => x.Id == 1).FirstOrDefault();
            ViewBag.Photo1 = db.MiddleBlockPhotoes.Where(x => x.Id == 1).FirstOrDefault().Photo;
            ViewBag.Photo2 = db.MiddleBlockPhotoes.Where(x => x.Id == 2).FirstOrDefault().Photo;
            ViewBag.Photo3 = db.MiddleBlockPhotoes.Where(x => x.Id == 3).FirstOrDefault().Photo;
            ViewBag.P1 = db.BottomblockPhotoes.Where(x => x.Id == 1).FirstOrDefault().Photo;
            ViewBag.P2 = db.BottomblockPhotoes.Where(x => x.Id == 2).FirstOrDefault().Photo;
            ViewBag.P3 = db.BottomblockPhotoes.Where(x => x.Id == 3).FirstOrDefault().Photo;
            ViewBag.P4 = db.BottomblockPhotoes.Where(x => x.Id == 4).FirstOrDefault().Photo;

           

            return View(db.SliderPhotoes.ToList());
        }
    }
}