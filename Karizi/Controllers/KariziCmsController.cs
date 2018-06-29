using Karizi.Helper;
using Karizi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Karizi.Filter;

namespace Karizi.Controllers

{
    [IfAuthorized]
    public class KariziCmsController : Controller
    {
        DB_A3C991_karizi2018Entities db = null;
        void CheckConnection()
        {
           if(db == null)
            {
                db = new DB_A3C991_karizi2018Entities();
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        //პროდუქციის დამატება
        public ActionResult Production()
        {
            CheckConnection();
            ViewBag.Name = db.Productions.Where(x => x.Active == false).ToList();
            return View();
        }
        public ActionResult AddProduction(string Name,HttpPostedFileBase Photo)
        {
            CheckConnection();
            if (Photo != null)
            {
                string name = MainHelper.Random32();
                db.Productions.Add(new Production()
                {
                    Name = Name,
                    Photo= "/images/ProductionMain/"+name+".jpeg",
                    Active = false,
                    CreateDate = DateTime.Now
                });
                db.SaveChanges();
                using (var newimage = ScaleImage(Image.FromStream(Photo.InputStream, true, true), 1000, 1000))
                {

                    string path = Server.MapPath("/images/ProductionMain/" + name + ".jpeg");

                    newimage.Save(path, ImageFormat.Jpeg);

                }
                TempData["Success"] = "წარმატებით დაემატა";
                return RedirectToAction("Production");
            }
            else
            {
                TempData["ErrorP"] = "აირჩიე პროდიქტის ფოტო";
                return RedirectToAction("Production");
            }
           
        }

        public ActionResult AddProductPhotos(int Id, IEnumerable<HttpPostedFileBase> Photo, string PhotoTitle)
        {
            CheckConnection();
            foreach(var item in Photo)
            {
                if(item != null)
                {
                    string name = MainHelper.Random32();
                    db.ProductionPhotoes.Add(new ProductionPhoto()
                    {
                        ProductionId = Id,
                        PhotoTitle=PhotoTitle,
                        Photo = "/images/Production/" + name + ".jpeg",
                        CreateDate=DateTime.Now
                    });
                    db.SaveChanges();
                    using (var newimage = ScaleImage(Image.FromStream(item.InputStream, true, true), 1000, 1000))
                    {

                        string path = Server.MapPath("/images/Production/" + name + ".jpeg");

                        newimage.Save(path, ImageFormat.Jpeg);

                    }
                }
                else
                {
                    TempData["Error"] = "აირჩიე ფოტო";
                    return RedirectToAction("Production");
                }
            }
            db.Productions.Where(x => x.Id == Id).FirstOrDefault().Active = true;
            db.SaveChanges();
            TempData["Photo"] = "ფოტოები წარმატებით დაემატა";
            return RedirectToAction("Production");
        }


        //ჩემი ნამუშევრების დამატება

        public ActionResult Portfolio()
        {
            CheckConnection();
            ViewBag.Name = db.Portfolios.Where(x => x.Active == false).ToList();
            return View();
        }

        public ActionResult AddPortfolio(string Name,HttpPostedFileBase Photo)
        {
            CheckConnection();
            if(Photo != null)
            {
                string name = MainHelper.Random32();
            db.Portfolios.Add(new Models.Portfolio()
            {
                Name=Name,
                Photo="/images/PortfolioMain/"+name+".jpeg",
                Active =false,
                CreateDate=DateTime.Now
            });
            db.SaveChanges();
                using (var newimage = ScaleImage(Image.FromStream(Photo.InputStream, true, true), 1000, 1000))
                {

                    string path = Server.MapPath("/images/PortfolioMain/" + name + ".jpeg");

                    newimage.Save(path, ImageFormat.Jpeg);

                }
                TempData["Success"] = "წარმატებით დაემატა";
            return RedirectToAction("Portfolio");
            }
            else
            {
                TempData["ErrorP"] = "აირჩიე სერვისის ფოტო";
                return RedirectToAction("Portfolio");
            }
        }

        public ActionResult AddPortfolioPhoto(int Id, IEnumerable<HttpPostedFileBase> Photo,string PhotoTitle)
        {
            CheckConnection();
            foreach(var item in Photo)
            {
                if (item != null)
                {
                    string name = MainHelper.Random32();
                    db.PortfolioPhotoes.Add(new PortfolioPhoto()
                    {
                        PortfolioId = Id,
                        PhotoTitle=PhotoTitle,
                        Photo = "/images/Portfolio/" + name + ".jpeg",
                        CreateDate=DateTime.Now,
                    });
                    db.SaveChanges();
                    using (var newimage = ScaleImage(Image.FromStream(item.InputStream, true, true), 1000, 1000))
                    {

                        string path = Server.MapPath("/images/Portfolio/" + name + ".jpeg");

                        newimage.Save(path, ImageFormat.Jpeg);

                    }

                }
                else
                {
                    TempData["Error"] = "აირჩიე ფოტო";
                    return RedirectToAction("Portfolio");
                }
            }
            db.Portfolios.Where(x => x.Id == Id).FirstOrDefault().Active = true;
            db.SaveChanges();
            TempData["Photo"] = "ფოტოები წარმატებით დაემატა";
            return RedirectToAction("Portfolio");
        }

        //სერვისების დამატება
        public ActionResult Services()
        {
            CheckConnection();
            ViewBag.Name = db.Services.Where(x => x.Active == false).ToList();
            return View();
        }

        public ActionResult AddService(string Name,HttpPostedFileBase Photo)
        {
            CheckConnection();
            if(Photo != null)
            {
            string name = MainHelper.Random32();
            db.Services.Add(new Service()
            {
                Name=Name,
                Photo= "/images/ServiceMain/"+name+".jpeg",
                Active =false,
                CreateDate=DateTime.Now,
            });
            db.SaveChanges();
                using (var newimage = ScaleImage(Image.FromStream(Photo.InputStream, true, true), 1000, 1000))
                {

                    string path = Server.MapPath("/images/ServiceMain/" + name + ".jpeg");

                    newimage.Save(path, ImageFormat.Jpeg);

                }
                TempData["Success"] = "წარმატებით დაემატა";
            return RedirectToAction("Services");
            }
            else
            {
                TempData["ErrorP"] = "აირჩიე სერვისის ფოტო";
                return RedirectToAction("Services");
            }
        }
        public ActionResult AddServicePhoto(int Id, IEnumerable<HttpPostedFileBase> Photo,string PhotoTitle)
        {
            CheckConnection();
            foreach(var item in Photo)
            {
                if(item != null)
                {
                    string name = MainHelper.Random32();
                    db.ServicesPhotoes.Add(new ServicesPhoto()
                    {
                        ServiceId=Id,
                        PhotoTitle=PhotoTitle,
                        Photo= "/images/Service/"+name+".jpeg",
                        CreateDate=DateTime.Now,
                    });
                    db.SaveChanges();
                    using (var newimage = ScaleImage(Image.FromStream(item.InputStream, true, true), 1000, 1000))
                    {

                        string path = Server.MapPath("/images/Service/" + name + ".jpeg");

                        newimage.Save(path, ImageFormat.Jpeg);

                    }
                }
                else
                {
                    TempData["Error"] = "აირჩიე ფოტო";
                    return RedirectToAction("Services");
                }
            }
            db.Services.Where(x => x.Id == Id).FirstOrDefault().Active = true;
            db.SaveChanges();
            TempData["Photo"] = "ფოტოები წარმატებით დაემატა";
            return RedirectToAction("Services");
        }

        // პროდუქციის შეცვლა წაშლა
        public ActionResult ChangeProduction()
        {
            CheckConnection();
            return View(db.Productions.Where(x=>x.Active==true).ToList());
        }

        [ValidateInput(false)]
        public ActionResult ChangeAddedProduct([DefaultValue(0)]int Id)
        {
            CheckConnection();
            if (Id == 0)
            {
                return RedirectToAction("ChangeProduction");
            }
            else { 
            ViewBag.ProductPhoto = db.ProductionPhotoes.Where(x => x.ProductionId == Id).ToList();
            ViewBag.Product = db.Productions.Where(x => x.Id == Id).FirstOrDefault();
            return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChangeAddedProduct(string Name,int Id,HttpPostedFileBase Photo)
        {
            CheckConnection();
            if(Photo != null)
            {
                var NewProduct = db.Productions.Where(x => x.Id == Id).FirstOrDefault();
                string name = MainHelper.Random32();
                string Pname = NewProduct.Photo;
                string fullpath = Request.MapPath(Pname);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                
                NewProduct.Photo = "/images/ProductionMain/" + name + ".jpeg";
                db.SaveChanges();
                using (var newimage = ScaleImage(Image.FromStream(Photo.InputStream, true, true), 1000, 1000))
                {

                    string path = Server.MapPath("/images/ProductionMain/" + name + ".jpeg");

                    newimage.Save(path, ImageFormat.Jpeg);

                }
                

            }
            var NewP = db.Productions.Where(x => x.Id == Id).FirstOrDefault();
            NewP.Name = Name;
            db.SaveChanges();
            TempData["Success"] = "წარმატებით შეიცვალა";
            return RedirectToAction("ChangeProduction");

        }
        [ValidateInput(false)]
        public ActionResult AddNewProductPhotos([DefaultValue(0)]int Id,IEnumerable<HttpPostedFileBase>Photo,string PhotoTitle)
        {
            CheckConnection();
            if(Id == 0)
            {
                return RedirectToAction("ChangeProduction");
            }

            foreach(var item in Photo)
            {
                if(item != null)
                {
                    string name = MainHelper.Random32();
                    db.ProductionPhotoes.Add(new ProductionPhoto()
                    {
                        ProductionId=Id,
                        PhotoTitle=PhotoTitle,
                        Photo= "/images/Production/"+name+".jpeg",
                        CreateDate=DateTime.Now,
                    });
                    db.SaveChanges();
                    using (var newimage = ScaleImage(Image.FromStream(item.InputStream, true, true), 1000, 1000))
                    {

                        string path = Server.MapPath("/images/Production/" + name + ".jpeg");

                        newimage.Save(path, ImageFormat.Jpeg);

                    }
                }
                else
                {
                    TempData["ErrorP"] = "აირჩიე პროდუქციის ფოტოები";
                    return RedirectToAction("ChangeAddedProduct",new { id=Id});
                }
            }
            TempData["Success"] = "ფოტოები წარმატებით დაემატა";
            return RedirectToAction("ChangeAddedProduct", new { id = Id });
        }
        [ValidateInput(false)]
        public ActionResult DelProductPhoto(int Id)
        {
            if (Id == 0)
            {
                return RedirectToAction("ChangeProduction");
            }
            CheckConnection();
            string Pname = db.ProductionPhotoes.Where(x => x.Id == Id).FirstOrDefault().Photo;
            string fullPath = Request.MapPath(Pname);
            ViewBag.Id = db.ProductionPhotoes.Where(x => x.Id == Id).FirstOrDefault().ProductionId;
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            db.ProductionPhotoes.Remove(db.ProductionPhotoes.Where(x => x.Id == Id).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction("ChangeAddedProduct", new { id = ViewBag.Id });
        }

        public ActionResult DelProduction(int id)
        {
            CheckConnection();
            foreach(var item in db.ProductionPhotoes.Where(x => x.ProductionId == id))
            {
                string Pname = item.Photo;
                string fullpath = Request.MapPath(Pname);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
            }
            
            string Covername = db.Productions.Where(x => x.Id == id).FirstOrDefault().Photo;
            string full = Request.MapPath(Covername);
            if (System.IO.File.Exists(full))
            {
                System.IO.File.Delete(full);
            }
            db.ProductionPhotoes.RemoveRange(db.ProductionPhotoes.Where(x => x.ProductionId == id));
            db.Productions.Remove(db.Productions.Where(x => x.Id == id).FirstOrDefault());
            db.SaveChanges();
            TempData["Success"] = "პროდუქცია წაიშალა";
            return RedirectToAction("ChangeProduction");
        }

       //სერვისების შეცვლა წაშლა

        public ActionResult ChangeService()
        {
            CheckConnection();
            return View(db.Services.Where(x=>x.Active==true).ToList());
        }

        [ValidateInput(false)]
        public ActionResult ChangeAddedService([DefaultValue(0)]int Id)
        {
            CheckConnection();
            if (Id == 0)
            {
                return RedirectToAction("ChangeService");
            }
            else
            {
                ViewBag.ServicePhoto = db.ServicesPhotoes.Where(x => x.ServiceId == Id).ToList();
                ViewBag.Service = db.Services.Where(x => x.Id == Id).FirstOrDefault();
                return View();
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChangeAddedService(string Name, int Id, HttpPostedFileBase Photo)
        {
            CheckConnection();
            if (Photo != null)
            {
                var NewProduct = db.Services.Where(x => x.Id == Id).FirstOrDefault();
                string name = MainHelper.Random32();
                string Pname = NewProduct.Photo;
                string fullpath = Request.MapPath(Pname);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                
                NewProduct.Photo = "/images/ServiceMain/" + name + ".jpeg";
                db.SaveChanges();
                using (var newimage = ScaleImage(Image.FromStream(Photo.InputStream, true, true), 1000, 1000))
                {

                    string path = Server.MapPath("/images/ServiceMain/" + name + ".jpeg");

                    newimage.Save(path, ImageFormat.Jpeg);

                }
                

            }
            var NewP = db.Services.Where(x => x.Id == Id).FirstOrDefault();
            NewP.Name = Name;
            db.SaveChanges();
            TempData["Success"] = "წარმატებით შეიცვალა";
            return RedirectToAction("ChangeService");
        }


        [ValidateInput(false)]
        public ActionResult AddNewServicePhotos([DefaultValue(0)]int Id, IEnumerable<HttpPostedFileBase> Photo,string PhotoTitle)
        {
            CheckConnection();
            if (Id == 0)
            {
                return RedirectToAction("ChangeService");
            }

            foreach (var item in Photo)
            {
                if (item != null)
                {
                    string name = MainHelper.Random32();
                    db.ServicesPhotoes.Add(new ServicesPhoto()
                    {
                        ServiceId = Id,
                        PhotoTitle=PhotoTitle,
                        Photo = "/images/Service/" + name + ".jpeg",
                        CreateDate = DateTime.Now,
                    });
                    db.SaveChanges();
                    using (var newimage = ScaleImage(Image.FromStream(item.InputStream, true, true), 1000, 1000))
                    {

                        string path = Server.MapPath("/images/Service/" + name + ".jpeg");

                        newimage.Save(path, ImageFormat.Jpeg);

                    }
                }
                else
                {
                    TempData["ErrorP"] = "აირჩიე სერვისის ფოტოები";
                    return RedirectToAction("ChangeAddedService", new { id = Id });
                }
            }
            TempData["Success"] = "ფოტოები წარმატებით დაემატა";
            return RedirectToAction("ChangeAddedService", new { id = Id });
        }
        [ValidateInput(false)]
        public ActionResult DelServicePhoto(int Id)
        {
            if (Id == 0)
            {
                return RedirectToAction("ChangeService");
            }
            CheckConnection();
            string Pname = db.ServicesPhotoes.Where(x => x.Id == Id).FirstOrDefault().Photo;
            string fullPath = Request.MapPath(Pname);
            ViewBag.Id = db.ServicesPhotoes.Where(x => x.Id == Id).FirstOrDefault().ServiceId;
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            db.ServicesPhotoes.Remove(db.ServicesPhotoes.Where(x => x.Id == Id).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction("ChangeAddedService", new { id = ViewBag.Id });
        }

        public ActionResult DelService(int id)
        {
            CheckConnection();
            foreach (var item in db.ServicesPhotoes.Where(x => x.ServiceId == id))
            {
                string Pname = item.Photo;
                string fullpath = Request.MapPath(Pname);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
            }

            string Covername = db.Services.Where(x => x.Id == id).FirstOrDefault().Photo;
            string full = Request.MapPath(Covername);
            if (System.IO.File.Exists(full))
            {
                System.IO.File.Delete(full);
            }
            db.ServicesPhotoes.RemoveRange(db.ServicesPhotoes.Where(x => x.ServiceId == id));
            db.Services.Remove(db.Services.Where(x => x.Id == id).FirstOrDefault());
            db.SaveChanges();
            TempData["Success"] = "სერვისი წაიშალა";
            return RedirectToAction("ChangeService");
        }

        //ნამუშევრების შეცვლა წაშლა
        public ActionResult ChangePortfolio()
        {
            CheckConnection();
            return View(db.Portfolios.Where(x => x.Active == true).ToList());
        }

        [ValidateInput(false)]
        public ActionResult ChangeAddedPortfolio([DefaultValue(0)]int Id)
        {
            CheckConnection();
            if (Id == 0)
            {
                return RedirectToAction("ChangePortfolio");
            }
            else
            {
                ViewBag.PortfolioPhoto = db.PortfolioPhotoes.Where(x => x.PortfolioId == Id).ToList();
                ViewBag.Portfolio = db.Portfolios.Where(x => x.Id == Id).FirstOrDefault();
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChangeAddedPortfolio(string Name, int Id, HttpPostedFileBase Photo)
        {
            CheckConnection();
            if (Photo != null)
            {
                var NewProduct = db.Portfolios.Where(x => x.Id == Id).FirstOrDefault();
                string name = MainHelper.Random32();
                string Pname = NewProduct.Photo;
                string fullpath = Request.MapPath(Pname);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                
                NewProduct.Photo = "/images/PortfolioMain/" + name + ".jpeg";
                db.SaveChanges();
                using (var newimage = ScaleImage(Image.FromStream(Photo.InputStream, true, true), 1000, 1000))
                {

                    string path = Server.MapPath("/images/PortfolioMain/" + name + ".jpeg");

                    newimage.Save(path, ImageFormat.Jpeg);

                }
               
               

            }
            var newP = db.Portfolios.Where(x => x.Id == Id).FirstOrDefault();
            newP.Name = Name;
            db.SaveChanges();
            TempData["Success"] = "წარმატებით შეიცვალა";
            return RedirectToAction("ChangePortfolio");

        }
        [ValidateInput(false)]
        public ActionResult AddNewPortfolioPhotos([DefaultValue(0)]int Id, IEnumerable<HttpPostedFileBase> Photo,string PhotoTitle)
        {
            CheckConnection();
            if (Id == 0)
            {
                return RedirectToAction("ChangePortfolio");
            }

            foreach (var item in Photo)
            {
                if (item != null)
                {
                    string name = MainHelper.Random32();
                    db.PortfolioPhotoes.Add(new PortfolioPhoto()
                    {
                        PortfolioId = Id,
                        PhotoTitle=PhotoTitle,
                        Photo = "/images/Portfolio/" + name + ".jpeg",
                        CreateDate = DateTime.Now,
                    });
                    db.SaveChanges();
                    using (var newimage = ScaleImage(Image.FromStream(item.InputStream, true, true), 1000, 1000))
                    {

                        string path = Server.MapPath("/images/Portfolio/" + name + ".jpeg");

                        newimage.Save(path, ImageFormat.Jpeg);

                    }
                }
                else
                {
                    TempData["ErrorP"] = "აირჩიე ნამუშევრების ფოტოები";
                    return RedirectToAction("ChangeAddedPortfolio", new { id = Id });
                }
            }
            TempData["Success"] = "ფოტოები წარმატებით დაემატა";
            return RedirectToAction("ChangeAddedPortfolio", new { id = Id });
        }
        [ValidateInput(false)]
        public ActionResult DelPortfolioPhoto(int Id)
        {
            if (Id == 0)
            {
                return RedirectToAction("ChangePortfolio");
            }
            CheckConnection();
            string Pname = db.PortfolioPhotoes.Where(x => x.Id == Id).FirstOrDefault().Photo;
            string fullPath = Request.MapPath(Pname);
            ViewBag.Id = db.PortfolioPhotoes.Where(x => x.Id == Id).FirstOrDefault().PortfolioId;
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            db.PortfolioPhotoes.Remove(db.PortfolioPhotoes.Where(x => x.Id == Id).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction("ChangeAddedPortfolio", new { id = ViewBag.Id });
        }

        public ActionResult DelPortfolio(int id)
        {
            CheckConnection();
            foreach (var item in db.PortfolioPhotoes.Where(x => x.PortfolioId == id))
            {
                string Pname = item.Photo;
                string fullpath = Request.MapPath(Pname);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
            }

            string Covername = db.Portfolios.Where(x => x.Id == id).FirstOrDefault().Photo;
            string full = Request.MapPath(Covername);
            if (System.IO.File.Exists(full))
            {
                System.IO.File.Delete(full);
            }
            db.PortfolioPhotoes.RemoveRange(db.PortfolioPhotoes.Where(x => x.PortfolioId == id));
            db.Portfolios.Remove(db.Portfolios.Where(x => x.Id == id).FirstOrDefault());
            db.SaveChanges();
            TempData["Success"] = "პროდუქცია წაიშალა";
            return RedirectToAction("ChangePortfolio");
        }

        //სიახლეების დამატება
        public ActionResult AddNews()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNews(NewsModel model,HttpPostedFileBase Photo)
        {
            CheckConnection();
            if (Photo != null)
            {
                string name = MainHelper.Random32();
                db.News.Add(new News()
                {
                    Title = model.Title,
                    Photo = "/images/News/" + name + ".jpeg",
                    Text=model.Text,
                    CreateDate = DateTime.Now
                });
                db.SaveChanges();
                using (var newimage = ScaleImage(Image.FromStream(Photo.InputStream, true, true), 1000, 1000))
                {

                    string path = Server.MapPath("/images/News/" + name + ".jpeg");

                    newimage.Save(path, ImageFormat.Jpeg);

                }
                TempData["Success"] = "სიახლე წარმატებით დაემატა";
                return RedirectToAction("AddNews");
            }
            else
            {
                TempData["Error"] = "გთხოვთ აირჩიოთ სიახლის ფოტო";
                return RedirectToAction("AddNews");
            }

        }

        //სიახლის შეცვლა წაშლა
         
        public ActionResult EditNews(NewsModel model,HttpPostedFileBase Photo)
        {
            CheckConnection();
            return View(db.News.ToList());
        }

        [ValidateInput(false)]
        public ActionResult EditAddedNews([DefaultValue(0)]int id)
        {
            CheckConnection();
            if (db.News.Any(x => x.Id == id)) { 
            ViewBag.News = db.News.Where(x => x.Id == id).FirstOrDefault();
            return View();
            }
            else
            {
                return RedirectToAction("EditNews");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditAddedNews(NewsModel model,HttpPostedFileBase Photo,int Id)
        {
            CheckConnection();
            if (string.IsNullOrWhiteSpace(model.Text))
            {
                TempData["Faild"] = "შეავსეთ ყველა ველი";
                return RedirectToAction("EditAddedNews", new { id = Id });
            }
            if(model.Text.Length > 4000)
            {
                TempData["Faild"] = "ტექსტი უნდა იყოს 0-ზე მეტი და 4000-ზე ნაკლები";
                return RedirectToAction("EditAddedNews", new { id = Id });
            }

            if(Photo != null)
            {
                var nNews = db.News.Where(x => x.Id == Id).FirstOrDefault();
                string name = MainHelper.Random32();
                string Pname = db.News.Where(x => x.Id == Id).FirstOrDefault().Photo;
                string fullpath = Request.MapPath(Pname);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                nNews.Photo = "/images/News/" + name + ".jpeg";
               
                db.SaveChanges();
                using (var newimage = ScaleImage(Image.FromStream(Photo.InputStream, true, true), 1000, 1000))
                {

                    string path = Server.MapPath("/images/News/" + name + ".jpeg");

                    newimage.Save(path, ImageFormat.Jpeg);
                   
                    
                }

            }


                var New = db.News.Where(x => x.Id == Id).FirstOrDefault();
                New.Title = model.Title;
                New.Text = model.Text;
                db.SaveChanges();
                TempData["Success"]= "წარმატებით შეიცვალა";
                return RedirectToAction("EditNews");
            

            
        }

        [ValidateInput(false)]
        public ActionResult DelNews([DefaultValue(0)]int id)
        {
            CheckConnection();
            if (db.News.Any(x => x.Id == id))
            {
                string Pname = db.News.Where(x => x.Id == id).FirstOrDefault().Photo;
                string fullpath = Request.MapPath(Pname);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                db.News.Remove(db.News.Where(x => x.Id == id).FirstOrDefault());
                db.SaveChanges();
                TempData["Success"] = "სიახლე წაიშალა";
                return RedirectToAction("EditNews");
            }
            else
            {
                return RedirectToAction("EditNews");
            }
        }

        //ჩვენს შესახებ
        public ActionResult AboutUs()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult AboutUs(string Text, IEnumerable<HttpPostedFileBase> Photo)
        {
            CheckConnection();
            foreach(var item in Photo)
            {
                string name = MainHelper.Random32();
                db.AboutUsPhotoes.Add(new AboutUsPhoto()
                {
                    Photo = "/images/AboutUs/" + name + ".jpeg",
                    CreateDate=DateTime.Now
                });
                db.SaveChanges();
                using (var newimage = ScaleImage(Image.FromStream(item.InputStream, true, true), 1000, 1000))
                {

                    string path = Server.MapPath("/images/AboutUs/" + name + ".jpeg");

                    newimage.Save(path, ImageFormat.Jpeg);


                }
            }
            db.Abouts.Add(new About()
            {
                Text=Text,
                CreateDate =DateTime.Now
            });
            db.SaveChanges();
            TempData["Success"] = "წარმატებით დაემატა";
            return RedirectToAction("AboutUs");
        }

        public ActionResult EditAbout()
        {
            CheckConnection();
            ViewBag.About = db.Abouts.Where(x =>x.Id==1).FirstOrDefault();
            return View(db.AboutUsPhotoes.ToList());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditAbout(string Text)
        {
            CheckConnection();
            db.Abouts.Where(x => x.Id == 1).FirstOrDefault().Text = Text;
            db.SaveChanges();
            TempData["Success"] = "წარმატებით შეიცვალა ტექსტი";
            return RedirectToAction("EditAbout");
        }
        public ActionResult AddAboutPhoto(IEnumerable<HttpPostedFileBase> Photo)
        {
            CheckConnection();
            foreach(var item in Photo)
            {
                if (item != null)
                {
                    string name = MainHelper.Random32();

                    db.AboutUsPhotoes.Add(new AboutUsPhoto()
                    {
                        Photo = "/images/AboutUs/" + name + ".jpeg",
                        CreateDate = DateTime.Now
                    });
                    db.SaveChanges();
                    using (var newimage = ScaleImage(Image.FromStream(item.InputStream, true, true), 1000, 1000))
                    {

                        string path = Server.MapPath("/images/AboutUs/" + name + ".jpeg");

                        newimage.Save(path, ImageFormat.Jpeg);


                    }

                }
                else
                {
                    TempData["Faild"] = "აირჩიე ჩვენს შესახებ ფოტო";
                    return RedirectToAction("EditAbout");
                }
            }
            TempData["Success"] = "ფოტოები წარმატებით დაემატა";
            
            return RedirectToAction("EditAbout");
        }

        public ActionResult DelAboutPhoto([DefaultValue(0)]int id)
        {
            CheckConnection();
            if (db.AboutUsPhotoes.Any(x => x.Id == id))
            {
                string Pname = db.AboutUsPhotoes.Where(x => x.Id == id).FirstOrDefault().Photo;
                string fullpath = Request.MapPath(Pname);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                db.AboutUsPhotoes.Remove(db.AboutUsPhotoes.Where(x => x.Id == id).FirstOrDefault());
                db.SaveChanges();
                return RedirectToAction("EditAbout");
            }
            else
            {
                return RedirectToAction("EditAbout");
            }
        }

        //მთავარი
        public ActionResult Main()
        {
            

            return View();
        }
        [HttpPost]
        public ActionResult Main(string TextMid,string TextBot)
        {
            CheckConnection();
            db.MiddleBlockTexts.Add(new MiddleBlockText()
            {
                Text=TextMid,
                CreateDate=DateTime.Now

            });
            db.BottomblockTexts.Add(new BottomblockText()
            {
                Text=TextBot,
                CreateDate=DateTime.Now
            });
            db.SaveChanges();
            TempData["Success"] = "წარმატებით დაემატა";
            return RedirectToAction("Main");
        }

        public ActionResult EditMain()
        {
            CheckConnection();
            ViewBag.Mid = db.MiddleBlockTexts.Where(x => x.Id == 1).FirstOrDefault();
            ViewBag.Bot = db.BottomblockTexts.Where(x => x.Id == 1).FirstOrDefault();
            ViewBag.Photo1 = db.MiddleBlockPhotoes.Where(x => x.Id == 1).FirstOrDefault().Photo;
            ViewBag.Photo2= db.MiddleBlockPhotoes.Where(x => x.Id == 2).FirstOrDefault().Photo;
            ViewBag.Photo3 = db.MiddleBlockPhotoes.Where(x => x.Id == 3).FirstOrDefault().Photo;
            ViewBag.P1 = db.BottomblockPhotoes.Where(x => x.Id == 1).FirstOrDefault().Photo;
            ViewBag.P2 = db.BottomblockPhotoes.Where(x => x.Id == 2).FirstOrDefault().Photo;
            ViewBag.P3 = db.BottomblockPhotoes.Where(x => x.Id == 3).FirstOrDefault().Photo;
            ViewBag.P4 = db.BottomblockPhotoes.Where(x => x.Id == 4).FirstOrDefault().Photo;
            return View(db.SliderPhotoes.ToList());
        }
        [HttpPost]
        public ActionResult EditMain(string TextMid, string TextBot)
        {
            CheckConnection();
            db.MiddleBlockTexts.Where(x => x.Id == 1).FirstOrDefault().Text = TextMid;
            db.BottomblockTexts.Where(x => x.Id == 1).FirstOrDefault().Text = TextBot;
            db.SaveChanges();
            TempData["Success"] = "წარმატებით შეიცვალა";
            return RedirectToAction("EditMain");
        }

        public ActionResult MidPhoto(HttpPostedFileBase PhotoOne, HttpPostedFileBase PhotoTwo, HttpPostedFileBase PhotoThree)
        {
            CheckConnection();
            if(PhotoOne != null)
            {
                string Pname = db.MiddleBlockPhotoes.Where(x => x.Id == 1).FirstOrDefault().Photo;
                string fullpath = Request.MapPath(Pname);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                string name = MainHelper.Random32();
                db.MiddleBlockPhotoes.Where(x => x.Id == 1).FirstOrDefault().Photo = "/images/MidPhoto/" + name + ".jpeg";
                db.SaveChanges();
                using (var newimage = ScaleImage(Image.FromStream(PhotoOne.InputStream, true, true), 1000, 1000))
                {

                    string path = Server.MapPath("/images/MidPhoto/" + name + ".jpeg");

                    newimage.Save(path, ImageFormat.Jpeg);


                }
            }
            if (PhotoTwo != null)
            {
                string Pname = db.MiddleBlockPhotoes.Where(x => x.Id == 2).FirstOrDefault().Photo;
                string fullpath = Request.MapPath(Pname);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                string name = MainHelper.Random32();
                db.MiddleBlockPhotoes.Where(x => x.Id == 2).FirstOrDefault().Photo = "/images/MidPhoto/" + name + ".jpeg";
                db.SaveChanges();
                using (var newimage = ScaleImage(Image.FromStream(PhotoTwo.InputStream, true, true), 1000, 1000))
                {

                    string path = Server.MapPath("/images/MidPhoto/" + name + ".jpeg");

                    newimage.Save(path, ImageFormat.Jpeg);


                }

            }
            if (PhotoThree != null)
            {
                string Pname = db.MiddleBlockPhotoes.Where(x => x.Id == 3).FirstOrDefault().Photo;
                string fullpath = Request.MapPath(Pname);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                string name = MainHelper.Random32();
                db.MiddleBlockPhotoes.Where(x => x.Id == 3).FirstOrDefault().Photo = "/images/MidPhoto/" + name + ".jpeg";
                db.SaveChanges();
                using (var newimage = ScaleImage(Image.FromStream(PhotoThree.InputStream, true, true), 1000, 1000))
                {

                    string path = Server.MapPath("/images/MidPhoto/" + name + ".jpeg");

                    newimage.Save(path, ImageFormat.Jpeg);


                }
            }
            TempData["Success"] = "წარმატებით შეიცვალა ფოტო";
                return RedirectToAction("EditMain");
        }

        public ActionResult BotPhoto(HttpPostedFileBase Photo1, HttpPostedFileBase Photo2, HttpPostedFileBase Photo3, HttpPostedFileBase Photo4)
        {
            CheckConnection();
            if (Photo1 != null)
            {
                string Pname = db.BottomblockPhotoes.Where(x => x.Id == 1).FirstOrDefault().Photo;
                string fullpath = Request.MapPath(Pname);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                string name = MainHelper.Random32();
                db.BottomblockPhotoes.Where(x => x.Id == 1).FirstOrDefault().Photo = "/images/BotPhoto/" + name + ".jpeg";
                db.SaveChanges();
                using (var newimage = ScaleImage(Image.FromStream(Photo1.InputStream, true, true), 1000, 1000))
                {

                    string path = Server.MapPath("/images/BotPhoto/" + name + ".jpeg");

                    newimage.Save(path, ImageFormat.Jpeg);


                }
            }
            if (Photo2 != null)
            {
                string Pname = db.BottomblockPhotoes.Where(x => x.Id == 2).FirstOrDefault().Photo;
                string fullpath = Request.MapPath(Pname);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                string name = MainHelper.Random32();
                db.BottomblockPhotoes.Where(x => x.Id == 2).FirstOrDefault().Photo = "/images/BotPhoto/" + name + ".jpeg";
                db.SaveChanges();
                using (var newimage = ScaleImage(Image.FromStream(Photo2.InputStream, true, true), 1000, 1000))
                {

                    string path = Server.MapPath("/images/BotPhoto/" + name + ".jpeg");

                    newimage.Save(path, ImageFormat.Jpeg);


                }
            }
            if (Photo3 != null)
            {
                string Pname = db.BottomblockPhotoes.Where(x => x.Id == 3).FirstOrDefault().Photo;
                string fullpath = Request.MapPath(Pname);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                string name = MainHelper.Random32();
                db.BottomblockPhotoes.Where(x => x.Id == 3).FirstOrDefault().Photo = "/images/BotPhoto/" + name + ".jpeg";
                db.SaveChanges();
                using (var newimage = ScaleImage(Image.FromStream(Photo3.InputStream, true, true), 1000, 1000))
                {

                    string path = Server.MapPath("/images/BotPhoto/" + name + ".jpeg");

                    newimage.Save(path, ImageFormat.Jpeg);


                }
            }
            if (Photo4 != null)
            {
                string Pname = db.BottomblockPhotoes.Where(x => x.Id == 4).FirstOrDefault().Photo;
                string fullpath = Request.MapPath(Pname);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                string name = MainHelper.Random32();
                db.BottomblockPhotoes.Where(x => x.Id == 4).FirstOrDefault().Photo = "/images/BotPhoto/" + name + ".jpeg";
                db.SaveChanges();
                using (var newimage = ScaleImage(Image.FromStream(Photo4.InputStream, true, true), 1000, 1000))
                {

                    string path = Server.MapPath("/images/BotPhoto/" + name + ".jpeg");

                    newimage.Save(path, ImageFormat.Jpeg);


                }
            }
            TempData["Success"] = "წარმატებით შეიცვალა ფოტო";
            return RedirectToAction("EditMain");
        }

        public ActionResult sliderPhoto(IEnumerable<HttpPostedFileBase> Photo)
        {
            CheckConnection();
            foreach(var item in Photo)
            {
                if (item != null)
                {
                    string name = MainHelper.Random32();
                    db.SliderPhotoes.Add(new SliderPhoto()
                    {
                        Photo = "/images/SliderPhoto/" + name + ".jpeg",
                        CreateDate=DateTime.Now
                    });
                    db.SaveChanges();
                    using (var newimage = ScaleImage(Image.FromStream(item.InputStream, true, true), 1000, 1000))
                    {

                        string path = Server.MapPath("/images/SliderPhoto/" + name + ".jpeg");

                        newimage.Save(path, ImageFormat.Jpeg);


                    }
                }
                else
                {
                    TempData["Photo"] = "აირჩიე სლაიდერის ფოტოები";
                    return RedirectToAction("EditMain");
                }
            }
            TempData["Success"] = "სლაიდერის ფოტოები წარმატებით დაემატა";
            return RedirectToAction("EditMain");
        }
        [ValidateInput(false)]
        public ActionResult DelSliderPhoto([DefaultValue(0)]int id)
        {
            CheckConnection();
            if (db.SliderPhotoes.Any(x => x.Id == id))
            { 
              string Pname = db.SliderPhotoes.Where(x => x.Id == id).FirstOrDefault().Photo;
              string fullpath = Request.MapPath(Pname);
              if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                db.SliderPhotoes.Remove(db.SliderPhotoes.Where(x => x.Id == id).FirstOrDefault());
                db.SaveChanges();
                TempData["Success"] = "სლაიდერის ფოტო წაიშალა";
                return RedirectToAction("EditMain");
            }
            else
            {
                return RedirectToAction("EditMain");
            }
        }

        //კონტაქტი
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Contact(string Phone,string Address,string FbLink)
        {
            CheckConnection();
            db.Contacts.Add(new Models.Contact()
            {
                Phone=Phone,
                Address=Address,
                FbLink=FbLink,
            });
            db.SaveChanges();
            return RedirectToAction("Contact");
        }

        public ActionResult ChangeContact()
        {
            CheckConnection();

            return View(db.Contacts.Where(x=>x.Id==1).FirstOrDefault());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Change(string Phone, string Address, string FbLink)
        {

            CheckConnection();
            var Ncontact = db.Contacts.Where(x => x.Id == 1).FirstOrDefault();
            Ncontact.Phone = Phone;
            Ncontact.Address = Address;
            Ncontact.FbLink = FbLink;
            db.SaveChanges();
            TempData["Success"] = "წარმატებით შეიცვალა";
            return RedirectToAction("ChangeContact");
        }

        public ActionResult Logout()
        {
            CheckConnection();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }
    }
}