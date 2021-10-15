using Proiect_Online_Shopping_Anania_Anamaria.Database;
using Proiect_Online_Shopping_Anania_Anamaria.Models.HOME;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_Online_Shopping_Anania_Anamaria.Controllers
{
    public class HomeController : Controller

    {
        AEOnlineShoppingEntities ctx = new AEOnlineShoppingEntities();
        public ActionResult Index(string search,int? page)
        {
            HOMEIndexViewModels model = new HOMEIndexViewModels();
            return View(model.CreateModel(search,4,page));
        }
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult CheckoutDetails()
        {
            return View();
        }
        public ActionResult DecreaseQty(int ProductId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = ctx.Products.Find(ProductId);
                foreach(var item in cart)
                {
                    if (item.Product.ProductId == ProductId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item(){
                                Product=product,
                                    Quantity=prevQty-1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");
        }
        public ActionResult AddToCart(int ProductId)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = ctx.Products.Find(ProductId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var count = cart.Count();
                var product = ctx.Products.Find(ProductId);
                for (int i = 0; i < count; i++)
                {
                    if (cart[i].Product.ProductId == ProductId)
                    {
                        int prevQty = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cart.Where(x => x.Product.ProductId == ProductId).SingleOrDefault();
                        if (prd == null)
                        {
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });

                        }
                    }
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index", "Home");
        }
                
        public ActionResult RemoveFromCart(int ProductId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.Product.ProductId == ProductId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            
            Session["cart"] = cart;
            return Redirect("Index");
        }
    }
}