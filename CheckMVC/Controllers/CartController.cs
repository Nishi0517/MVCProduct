using CheckMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CheckMVC.Controllers
{
    public class CartController : Controller
    {
        ProductDBEntities db = new ProductDBEntities();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddToCart(int ProductID)
        {
            if (Session["cart"] == null) 
            {
                List<Item> cart = new List<Item>();

                 cart.Add(new Item() { Product = db.tblProducts.Find(ProductID), Quantity = 1 });

                //Item item = new Item { Product = db.tblProducts.Find(ProductID), Quantity = 1 }; // both are working

                //Item item = new Item();
                //item.Product = db.tblProducts.Find(ProductID);
                //item.Quantity = 1;
                //cart.Add(item); // both code are working
                

                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>) Session["cart"];
                int index = IsInCart(ProductID);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item() { Product = db.tblProducts.Find(ProductID), Quantity = 1 });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index", new { ProductID = ProductID });
        }

        public ActionResult RemoveFromCart(int ProductID)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = IsInCart(ProductID);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        public int IsInCart(int ProductID)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for(int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.prod_id == ProductID)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}