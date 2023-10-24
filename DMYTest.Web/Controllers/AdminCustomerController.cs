
namespace DMYTest.Web.Controllers
{
    #region Usings
    using DMYTest.Data.Context;
    using DMYTest.Data.Models;
    using PagedList;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion
    public class AdminCustomerController : Controller
    {
        // GET: Customer
        DMYDBContext db = new DMYDBContext();
        public ActionResult Index(int pageNumber = 1)
        {
            var customers = db.Customers.ToList().ToPagedList(pageNumber , 4);
            return View(customers);
        }
        [Authorize(Roles="admin")]
        public ActionResult Create()
        {

            List<SelectListItem> items = (from i in db.Users.ToList()
                                          select new SelectListItem
                                          {
                                              Value = i.ID.ToString(),
                                              Text = i.UserName,
                                          }).ToList();
            ViewBag.cstmr = items;
            return View();
        }

        [HttpPost]
        [Authorize(Roles="admin")]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {

                customer.User = db.Users.FirstOrDefault(u=>u.Email == User.Identity.Name);
                if (customer.User != null)
                {
                    
                    db.Customers.Add(customer);
                    db.SaveChanges();
                }
                ModelState.AddModelError("","Modelinizi yanlis girdiniz");
                return View("Index");
            }
            return View();

        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public JsonResult Delete(int customerId)
        {
            try
            {
                var customer = db.Customers.Find(customerId);
                if (customer != null)
                {
                    db.Customers.Remove(customer);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Musteri Silindi" });
                }
                else
                {
                    return Json(new { success = false , message = "Musteri Bulunamadi!"});
                }
            }
            catch (System.Exception e)
            {

                return Json(new {success = false , message  = e.Message });
            }
            
        }
    }
}