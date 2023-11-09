namespace DMYTest.Web.Controllers
{
    #region Using 
    using DMYTest.Data.Concrete;
    using System.Web.Mvc;
    using PagedList;
    using DMYTest.Data.Context;
    using System.Linq;
    using System;
    #endregion
    public class AdminOrderController : Controller
    {
        OrderRepository repository = new OrderRepository();
        DMYDBContext db = new DMYDBContext();

        [Authorize(Roles = "admin")]
        public ActionResult Index(int pageNumber = 1)
        {
            return View(repository.List().ToPagedList(pageNumber ,4));
        }

        [Authorize(Roles="admin")]
        public JsonResult DeleteById(int orderID)
        {
            try
            {
                var orderToDelete = db.Orders.Find(orderID);
                var salesToDelete = db.Sales.Where(S => S.Order.OrderID == orderID);
                if (orderToDelete != null)
                {
                    foreach (var item in orderToDelete.Sales)
                    {
                        db.Sales.Remove(item);
                        db.SaveChanges();
                    }
                    db.Orders.Remove(orderToDelete);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Product Deleted!" });

                }
                return Json(new { success = false, message = "Can't find the product to delete" });
                
            }
            catch (System.Exception e)
            {
                return Json(new { success = false , message = e.Message});
            }
        }
        [HttpPost]
        [Authorize(Roles ="admin")]
        public JsonResult SelectReceived(int orderID)
        {
            try
            {
                var orderToDelete = db.Orders.Find(orderID);
                if (orderToDelete != null)
                {
                    orderToDelete.OrderReceived = true;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Selected as received!" });
                }
                return Json(new { success = false, message = "Order not found!" });
            }
            catch (Exception e)
            {

                return Json(new { success = false, message = e.Message });
            }
        }

    }
}