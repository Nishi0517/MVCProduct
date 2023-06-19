using CheckMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace CheckMVC.Controllers
{
    public class HomeController : Controller
    {
        ProductDBEntities db = new ProductDBEntities();
        public ActionResult Index()
        {
            //List<tblProduct> products = db.tblProducts.ToList(); // also working
            List<tblProduct> products = (from data in db.tblProducts
                                         select data).ToList();
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}