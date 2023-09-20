
namespace DMYTest.Web.Controllers
{
    using Antlr.Runtime;
    #region Usings
    using DMYTest.Data.Context;
    using DMYTest.Data.Models;
    using DMYTest.Web.RazorView;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Services.Description;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    #endregion
    public class CartController : Controller
    {
        // GET: Cart
        InternDBContext context = new InternDBContext();
        public ActionResult Index(decimal? price)
        {

            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var user = context.Users.FirstOrDefault(x => x.Email == username);
                var model = context.Carts.Where(x => x.UserID == user.ID).ToList();
                var kid = context.Carts.FirstOrDefault(x => x.UserID == user.ID);
                ViewBag.userID = user.ID;
                if (model != null)
                {
                    if (kid == null)
                    {
                        ViewBag.Cost = "Sepetinizde Urun Bulunmamaktadir";
                    }
                    else if (kid != null)
                    {
                        price = context.Carts.Where(x => x.UserID == kid.UserID).Sum(x => x.Product.UnitPrice * x.Quantity);
                    }
                    return View(model);
                }


            }
            return HttpNotFound();

        }
        #region AddCart
        public ActionResult AddCart(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = context.Users.FirstOrDefault(U => U.Email == User.Identity.Name);
                var product = context.Products.Find(id);
                var modal = context.Carts.Where(c => c.UserID == user.ID).ToList();
                var basketIfExists = modal.FirstOrDefault(m => m.ProductID == product.ProductID);
                if (user != null)
                {
                    if (basketIfExists != null)
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
                    ModelState.AddModelError("", "Urun ekleme yetkiniz bulunmamaktadir lutfen dogru hesaba giris  yapiniz");
                    return View("login", "account");
                }
            }
            ModelState.AddModelError("", "Urun Eklemek icin once giris yapmalisiniz");
            return RedirectToAction("login", "account");
        }
        #endregion

        public ActionResult TotalCount(int? count)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = context.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
                count = context.Carts.Where(x => x.UserID == user.ID).Count();
                ViewBag.count = count;
                if (count == 0)
                {
                    ViewBag.count = "";
                }

            }
            return PartialView();
        }
        #region DeleteMethods
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
            return RedirectToAction("login", "account");

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
        #endregion

        #region JsonResult
        [HttpPost]
        public JsonResult IncreaseQuantity(int cartID)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    decimal totalPrice;
                    var user = context.Users.FirstOrDefault(U => U.Email == User.Identity.Name);
                    var cartToUpdate = context.Carts.Find(cartID);
                    var product = context.Products.FirstOrDefault(P => P.ProductID == cartToUpdate.ProductID);
                    var carts = context.Carts.Where(C => C.UserID == user.ID).ToList();

                    if (cartToUpdate.Quantity < product.Stock)
                    {
                        cartToUpdate.Quantity++;
                        cartToUpdate.Price = cartToUpdate.Quantity * product.UnitPrice;

                        context.SaveChanges();
                        totalPrice = TotalPriceCalculator(carts);

                        return Json(new { success = true, message = "Sepet Guncellendi", currentQuantity = cartToUpdate.Quantity, currentPrice = cartToUpdate.Price, totalPrice = totalPrice });
                    }
                    cartToUpdate.Quantity = product.Stock;
                    cartToUpdate.Price = cartToUpdate.Quantity * product.UnitPrice;
                    context.SaveChanges();
                    totalPrice = TotalPriceCalculator(carts);

                    return Json(new { success = true, message = "Maksimum stok Adedi kadar urunu sepete ekleyebilirsiniz !!", currentQuantity = cartToUpdate.Quantity, currentPrice = cartToUpdate.Price, totalPrice = totalPrice });

                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = "Hata" });

                }
            }
            return Json(new { success = false, message = "Oturumunuz kapali lutfen oturum aciniz" });


        }
        [HttpPost]
        public JsonResult DecreaseQuantity(int cartID)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    decimal totalPrice;
                    var cartToUpdate = context.Carts.Find(cartID);
                    var user = context.Users.FirstOrDefault(U => U.Email == User.Identity.Name);
                    var product = context.Products.FirstOrDefault(P => P.ProductID == cartToUpdate.ProductID);
                    var carts = context.Carts.Where(C => C.UserID == user.ID).ToList();
                    if (cartToUpdate.Quantity > 1)
                    {
                        cartToUpdate.Quantity--;
                        cartToUpdate.Price = cartToUpdate.Quantity * product.UnitPrice;
                        context.SaveChanges();
                        totalPrice = TotalPriceCalculator(carts);
                        return Json(new { success = true, message = "Sepet Guncellendi", currentQuantity = cartToUpdate.Quantity, currentPrice = cartToUpdate.Price, totalPrice = totalPrice });
                    }
                    else
                    {
                        cartToUpdate.Quantity = 1;
                        cartToUpdate.Price = cartToUpdate.Quantity * product.UnitPrice;
                        context.SaveChanges();
                        totalPrice = TotalPriceCalculator(carts);
                        return Json(new { success = true, message = "Urunun sepetteki adedi 0 olamaz!!", currentQuantity = cartToUpdate.Quantity, currentPrice = cartToUpdate.Price, totalPrice = totalPrice });

                    }
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = "Unexpected Error :" + e.Source });

                }
            }
            return Json(new { success = false, message = "Oturumunuz kapali lutfen giris yap kismindan oturumunuzu aciniz" });


        }
        #endregion
        #region PrivateMethods
        private decimal TotalPriceCalculator(List<Cart> carts, decimal totalPrice = 0)
        {

            for (int i = 0; i < carts.Count; i++)
            {
                totalPrice += carts[i].Price;
            }
            return totalPrice;
        }
        #endregion


    }

}