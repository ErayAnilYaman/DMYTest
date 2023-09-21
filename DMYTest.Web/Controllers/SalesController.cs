
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
    #endregion
    public class SalesController : Controller
    {
        // GET: Sales
        InternDBContext context = new InternDBContext();
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
                var model = context.Carts.FirstOrDefault(C => C.CartID == id);
                return View(model);
            }
            return HttpNotFound();

        }
        [Authorize]
        [HttpPost]
        public ActionResult BuyPost(Cart cart)
        {
            try
            {
                if (ModelState.IsValid && User.Identity.IsAuthenticated)
                {

                    var model = context.Carts.FirstOrDefault(x => x.CartID == cart.CartID);
                    var sale = new Sales
                    {
                        Date = DateTime.Now,
                        Image = model.Image,
                        Price = model.Price,
                        ProductID = model.ProductID,
                        Quantity = model.Quantity,
                        UserID = model.User.ID,

                    };
                    context.Carts.Remove(model);
                    context.Sales.Add(sale);
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
        public ActionResult BuyAll(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = context.Carts.Where(C => C.UserID == id);
                ViewBag.Cost = model.Sum(C => C.Price);
                return View(model.ToList());
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
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                var user = context.Users.FirstOrDefault(U => U.Email == username);
                var model = context.Carts.Where(X => X.UserID == user.ID).ToList();
                if (model != null)
                {
                    foreach (var item in model)
                    {
                        var sale = new Sales
                        {
                            Date = DateTime.Now,
                            Image = item.Image,
                            Price = item.Price,
                            ProductID = item.ProductID,
                            Quantity = item.Quantity,
                            UserID = user.ID,

                        };
                        context.Carts.Remove(item);
                        context.Sales.Add(sale);
                        context.SaveChanges();
                    }
                    ViewBag.progress = "Islem Basarili";
                    return RedirectToAction("index", "home");
                }

            }
            ModelState.AddModelError("", "Islem basarisiz");
            return View();
        }
        #endregion




    }
}