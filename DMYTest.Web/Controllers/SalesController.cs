
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
                var user = context.Users.FirstOrDefault(x=>x.Email == username);
                var model = context.Sales.Where(x => x.UserID == user.ID).ToList().ToPagedList(page,3);

                return View(model);

            }
            return View("login","account");
        }
        public ActionResult Buy(int id)
        {
            var model = context.Carts.FirstOrDefault(C=>C.CartID == id);
            ViewBag.CartID = id; 
            return View(model);
        }
        [HttpPost]
        public ActionResult Buy2(Sales sales)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = context.Carts.FirstOrDefault(x => x.CartID == sales.SalesID);
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
            }
            catch (Exception ex)
            {
                ViewBag.progress = "Satin alma islemi basarisiz";
                ModelState.AddModelError("", ex);
                throw;
            }
            return View("progress");

        }
    }
}