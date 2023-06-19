using CheckMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace CheckMVC.Controllers

{
    public class ProductController : Controller
    {
        ProductDBEntities db = new ProductDBEntities();
        // GET: Product
        public ActionResult Index()
        {
            List<tblProduct> ProductList = (from data in db.tblProducts
                                            select data).ToList();
            return View(ProductList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();   
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(tblProduct product, HttpPostedFileBase postedFile)
        {
            try
            {
                // TODO: Add insert logic here
                string extension = Path.GetExtension(postedFile.FileName);
                if (extension.Equals(".jpg") || extension.Equals(".png"))
                {
                    string fileName = "IMG" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + extension;
                    string savepath = Server.MapPath("~/Content/Images");
                    //postedFile.SaveAs(savepath + fileName);

                    postedFile.SaveAs(Path.Combine(savepath, fileName));
                    product.prod_pic = fileName;

                    db.tblProducts.Add(product);
                    db.SaveChanges();
                }
                else
                    return Content("<h1>You can only upload Jpg or Png formate</h1>");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            tblProduct product = db.tblProducts.Find(id);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(tblProduct product, HttpPostedFileBase postedFile)
        {
            try
            {
                // TODO: Add update logic here
                string filename = "";
                if (postedFile != null)
                {
                    string extension = Path.GetExtension(postedFile.FileName);

                    if (extension.Equals(".jpg") || extension.Equals(".png"))
                    {
                        string fileName = "IMG" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + extension;
                        string savepath = Server.MapPath("~/Content/Images");
                        //postedFile.SaveAs(savepath + fileName);

                        postedFile.SaveAs(Path.Combine(savepath, fileName));
                    }
                }
                tblProduct tblProduct = db.tblProducts.Find(product.prod_id);
                tblProduct.prod_name = product.prod_name;
                tblProduct.prod_price = product.prod_price;
                tblProduct.prod_qty = product.prod_qty;
                if (!filename.Equals(""))
                {
                    tblProduct.prod_pic = product.prod_pic;
                }
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            db.tblProducts.Remove(db.tblProducts.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
               
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
