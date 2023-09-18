
using System.Web.Mvc;

namespace DMYTest.Web.Controllers
{
    #region Usings
using DMYTest.Data.Concrete;
using System.Web.Mvc;
    using PagedList;
    using DMYTest.Data.Models;
    using System;
    using DMYTest.Data.Context;
    using System.Linq;
    #endregion
    public class AdminSalesController : Controller
    {
        InternDBContext db = new InternDBContext();
        SaleRepository srepository = new SaleRepository();
        public ActionResult Index(int pageNumber = 1)
        {
            var listedResults = srepository.List();
            return View(listedResults.ToPagedList(pageNumber , 4));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id) 
        {
            var saleToDelete = srepository.GetById(id);
            srepository.Delete(saleToDelete);
            return View("Index");
        }

        [HttpPost]
        public JsonResult IncreaseQuantity(int saleID)
        {
            try
            {
                var sale = srepository.GetById(saleID);
                var product = db.Products.FirstOrDefault(P => P.ProductID == sale.ProductID);
                if (sale.Quantity < product.Stock)
                {
                    sale.Quantity++;
                    sale.Price = sale.Quantity * product.UnitPrice;
                    sale.Date = DateTime.Now;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Satis guncellendi" , newPrice = sale.Price , newQuantity = sale.Quantity ,newDate = sale.Date });
                }
                return Json(new { success = false, error = "Satis Adedi stok bazinda olmalidir" });

            }
            catch (Exception e)
            {

                return Json(new { success = false, error = "Hata" });
            }
        }

        [HttpPost]
        public JsonResult DecreaseQuantity(int saleID)
        {
                try
                {
                    var sale = srepository.GetById(saleID);
                    var user = db.Users.FirstOrDefault(U => U.Email == User.Identity.Name);
                    var product = db.Products.FirstOrDefault(P => P.ProductID == sale.ProductID);
                    if (sale.Quantity >1)
                    {
                        sale.Quantity = sale.Quantity - 1;
                        sale.Price = sale.Quantity * product.UnitPrice;
                        sale.Date = DateTime.Now;
                        db.SaveChanges();
                        var newQuantity = sale.Quantity;

                        return Json(new {success = true , message = "Satis Guncellendi!!" , newPrice = sale.Price , newQuantity = sale.Quantity , newDate = sale.Date});
                    }

                    return Json(new { success = true, message = "Daha fazla urun satisa eklenemiyor!" ,newPrice = sale.Price  , newQuantity = sale.Quantity , newDate = sale.Date});
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                
                }
            
            
        }
                
    }
}