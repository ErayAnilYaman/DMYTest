
namespace DMYTest.Web.Controllers
{
    using Antlr.Runtime;
    #region Usings
    using DMYTest.Data.Context;
using DMYTest.Data.Models;
using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    #endregion
    public class CartController : Controller
    {
        // GET: Cart
        InternDBContext context = new InternDBContext();
        public ActionResult Index(decimal?price)
        {

            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var user = context.Users.FirstOrDefault(x => x.Email == username);
                var model = context.Carts.Where(x => x.UserID == user.ID).ToList();
                var kid = context.Carts.FirstOrDefault(x => x.UserID == user.ID);
                if (model !=null)
                {
                    if (kid == null)
                    {
                        ViewBag.Cost = "Sepetinizde Urun Bulunmamaktadir";
                    }
                    else if (kid!=null)
                    {
                        price = context.Carts.Where(x => x.UserID == kid.UserID).Sum(x => x.Product.UnitPrice * x.Quantity);
                        ViewBag.Cost = "Toplam Tutar =" + price + " TL";
                    }
                    return View(model);
                }


            }
            return HttpNotFound();

        }

        public ActionResult AddCart(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = context.Users.FirstOrDefault(U => U.Email == User.Identity.Name);
                var product = context.Products.Find(id);
                var modal = context.Carts.Where(c => c.UserID == user.ID).ToList();
                var basketIfExists = modal.FirstOrDefault(m => m.ProductID == product.ProductID);
                if (user!=null)
                {
                    if (basketIfExists!=null)
                    {
                        basketIfExists.Quantity++;
                        basketIfExists.Price = product.UnitPrice * basketIfExists.Quantity;
                        context.SaveChanges();
                        return RedirectToAction("Index", "Cart");
                    }
                    else
                    {
                        var s = new Cart
                        {
                            UserID = user.ID,
                            Date = DateTime.Now,
                            Image = product.Images.FirstOrDefault(i => i.ProductID == product.ProductID).ImagePath,
                            ProductID = product.ProductID,
                            Price = product.UnitPrice,
                            Quantity = 1,

                        };
                        context.Carts.Add(s);
                        context.SaveChanges();
                        return RedirectToAction("Index", "Cart");
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("","Urun ekleme yetkiniz bulunmamaktadir lutfen dogru hesaba giris  yapiniz");
                    return View("login", "account");
                }
            }
            ModelState.AddModelError("", "Urun Eklemek icin once giris yapmalisiniz");
            return RedirectToAction("login", "account");
        }
        public ActionResult TotalCount(int?count)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = context.Users.FirstOrDefault(x=>x.Email == User.Identity.Name);
                count = context.Carts.Where(x => x.UserID == user.ID).Count();
                ViewBag.count = count;
                if (count ==0)
                {
                    ViewBag.count = "";
                }

            }
            return PartialView();
        }
        
        public ActionResult Delete(int ID)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            if (User.Identity.IsAuthenticated && user != null)
            {
                var cartToDelete = context.Carts.Find(ID);
                context.Carts.Remove(cartToDelete);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("login","account");

        }
        public ActionResult DeleteAll()
        {
            var user = context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            if (User.Identity.IsAuthenticated && user != null)
            {
                var baskets = context.Carts.Where(c => c.UserID == user.ID).ToList();
                foreach (var item in baskets)
                {
                    context.Carts.Remove(item);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("login", "account");
        }
        
        

        [HttpPost]
        public JsonResult UpdateCart(int cartID, int newQuantity, decimal currentPrice)
        {
            try
            {
                
                var cart = context.Carts.FirstOrDefault(c => c.CartID == cartID);
                var product = context.Products.First(p => p.ProductID == cart.ProductID);
                if (cart != null)
                {
                    cart.Quantity = newQuantity;
                    cart.Price = cart.Quantity * product.UnitPrice;

                    context.SaveChanges(); // Değişiklikleri kaydet
                    currentPrice = cart.Price;
                }

                return Json(new { success = true, message = "Sepet güncellendi",newQuantity = newQuantity ,currentPrice = currentPrice });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        

    }

}