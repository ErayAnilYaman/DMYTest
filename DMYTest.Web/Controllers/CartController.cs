
namespace DMYTest.Web.Controllers
{
    using Antlr.Runtime;
    #region Usings
    using DMYTest.Data.Context;
    using DMYTest.Data.Models;
    using DMYTest.Web.RazorView;
    using Microsoft.Ajax.Utilities;
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
        DMYDBContext context = new DMYDBContext();

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
                        ViewBag.Cost = "You don't have any product at the cart";
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
                    ModelState.AddModelError("", "Please register");
                    return View("login", "account");
                }
            }
            ModelState.AddModelError("", "Please register");
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

        [Authorize]
        [HttpPost]
        public JsonResult DeleteAll()
        {
            
            var user = context.Users.FirstOrDefault(U => U.Email== User.Identity.Name);
            var carts = user.Carts.ToList();
            if (user !=null && carts !=null)
            {
                foreach (var cart in carts)
                {
                    context.Carts.Remove(cart);
                    context.SaveChanges();
                }
                return Json(new { success = true , message = "All of the products are deleted" });
            }
            return Json(new {success = false , message = "Couldn't find product to delete"});
        }
        
        [Authorize]
        [HttpPost]
        public JsonResult DeleteById(int cartID)
        {
            var user = context.Users.FirstOrDefault(U => U.Email == User.Identity.Name);
            var cartToDelete = user.Carts.FirstOrDefault(C => C.CartID == cartID);

            if (cartToDelete != null)
            {
                context.Carts.Remove(cartToDelete);
                context.SaveChanges();
                return Json(new { success = true, message = "Product has been deleted" });
            }
            return Json(new { success = false, message = "Couldn't find the product" });
        }
        #endregion

        #region QuantityMethodss
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

                        return Json(new { success = true, message = "Cart Updated", currentQuantity = cartToUpdate.Quantity, currentPrice = cartToUpdate.Price, totalPrice = totalPrice });
                    }
                    cartToUpdate.Quantity = product.Stock;
                    cartToUpdate.Price = cartToUpdate.Quantity * product.UnitPrice;
                    context.SaveChanges();
                    totalPrice = TotalPriceCalculator(carts);

                    return Json(new { success = true, message = "You can just add product to stock", currentQuantity = cartToUpdate.Quantity, currentPrice = cartToUpdate.Price, totalPrice = totalPrice });

                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = "Error" });

                }
            }
            return Json(new { success = false, message = "Please login" });


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
                        return Json(new { success = true, message = "Cart Updated", currentQuantity = cartToUpdate.Quantity, currentPrice = cartToUpdate.Price, totalPrice = totalPrice });
                    }
                    else
                    {
                        cartToUpdate.Quantity = 1;
                        cartToUpdate.Price = cartToUpdate.Quantity * product.UnitPrice;
                        context.SaveChanges();
                        totalPrice = TotalPriceCalculator(carts);
                        return Json(new { success = true, message = "Product count can not be 0", currentQuantity = cartToUpdate.Quantity, currentPrice = cartToUpdate.Price, totalPrice = totalPrice });

                    }
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = "Unexpected Error :" + e.Source });

                }
            }
            return Json(new { success = false, message = "Please register" });


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