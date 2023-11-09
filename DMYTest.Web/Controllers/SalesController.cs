
namespace DMYTest.Web.Controllers
{
    #region Usings
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using PagedList.Mvc;
    using PagedList;
    using DMYTest.Data.Context;
    using DMYTest.Data.Models;
    using System.ComponentModel;
    using System.Data.SqlClient;
    using System.Data.Entity.Core;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.Ajax.Utilities;
    using DMYTest.Data.Concrete;
    #endregion
    public class SalesController : Controller
    {
        // GET: Sales
        DMYDBContext context = new DMYDBContext();
        public ActionResult Index(int page = 1)
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var user = context.Users.FirstOrDefault(x => x.Email == username);
                var model = context.Sales.Where(x => x.UserID == user.ID).ToList().ToPagedList(page, 3);

                return View(model);

            }
            return RedirectToAction("login", "account");
        }
        #region Buy
        public ActionResult Buy(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = context.Users.First(U=>U.Email == User.Identity.Name);
                if (user.Customer != null)
                {
                    var model = context.Carts.FirstOrDefault(C => C.CartID == id);
                    return View(model);
                }
                return RedirectToAction("create","profile");
            }
            return HttpNotFound();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Buy(Cart cart)
        {
            try
            {
                if (ModelState.IsValid && User.Identity.IsAuthenticated)
                {
                    var user = context.Users.First(U => U.Email == User.Identity.Name);
                    var model = context.Carts.FirstOrDefault(x => x.CartID == cart.CartID);
                    List<Sales> sales = new List<Sales>
                    {
                        new Sales
                        {
                            Date = DateTime.Now,
                            Image = model.Image,
                            Price = model.Price,
                            ProductID = model.ProductID,
                            Quantity = model.Quantity,
                            UserID = model.User.ID,
                        }
                    };
                    var order = new Order
                    {
                        Address = user.Customer.Address,
                        OrderDate = DateTime.Now,
                        User = user,
                        Phone = user.Customer.Phone,
                        ProductOrderCrossModels = model.Product.ProductOrderCrossModels,
                        OrderReceived = false,
                        Sales = sales.ToList(),
                        UserID = user.ID
                    };
                    context.Carts.Remove(model);
                    context.Sales.Add(sales[0]);
                    context.Orders.Add(order);
                    context.SaveChanges();

                    ViewBag.progress = "Satin alma islemi basarili bir sekilde gerceklesmistir";
                }
                else
                {
                    return HttpNotFound();
                }
            }
            catch (SqlException ex)
            {
                ViewBag.progress = ex.Message;
                ModelState.AddModelError("", ex.Message);
                return View("progress");

            }
            return View("progress");

        }
        [Authorize]
        public ActionResult BuyAll()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = context.Users.First(U => U.Email == User.Identity.Name);
                if (user.Customer != null)
                {

                    var model = context.Carts.Where(C => C.User.ID == user.ID).ToList();
                    ViewBag.Cost = model.Sum(C => C.Price);
                    return View(model);
                }
                
            }
            return HttpNotFound();

        }

        [HttpPost]
        public ActionResult BuyAll(List<Cart> carts)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid )
            {
                var username = User.Identity.Name;
                var user = context.Users.FirstOrDefault(U => U.Email == username);
                var model = context.Carts.Where(X => X.UserID == user.ID).ToList();

                if (model != null && user.Customer != null)
                {
                    var order = new Order
                    {
                        Address = user.Customer.Address,
                        OrderDate = DateTime.Now,
                        OrderReceived = false,
                        Phone = user.Customer.Phone,
                        User = user,
                        UserID = user.ID,
                    };
                    var saleList = new List<Sales>();
                    foreach (var item in model)
                    {
                        var sale = new Sales
                        {
                            Date = DateTime.Now,
                            Image = item.Image,
                            Price = item.Price,
                            ProductID = item.ProductID,
                            Product = item.Product,
                            Quantity = item.Quantity,
                            UserID = user.ID,
                        };
                        order.ProductOrderCrossModels.Add(new ProductOrderCrossModel {
                            Order = order,
                            OrderID = order.OrderID,
                            Product = item.Product,
                            ProductID = item.Product.ProductID
                        });
                        context.Carts.Remove(item);
                        saleList.Add(sale);
                        context.SaveChanges();
                    }
                    order.Sales = saleList;
                    context.Orders.Add(order);
                    context.SaveChanges();
                    ViewBag.progress = "Islem Basarili";
                    return RedirectToAction("index", "home");
                }
                ModelState.AddModelError("", "Musteri Bilgilerinizi Doldurmaniz gerekiyor");
                return RedirectToAction("create","profile");
            }
            ModelState.AddModelError("", "Islem basarisiz");
            return View();
        }
        #endregion
        
    }
}