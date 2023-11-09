namespace DMYTest.Web.Controllers
{
    #region Usings
    using DMYTest.Data.Concrete;
    using DMYTest.Data.Context;
    using PagedList;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    #endregion
    public class HomeController : Controller
    {
        // GET: Home
        
        ProductRepository productRepository = new ProductRepository();
        DMYDBContext db = new DMYDBContext();
        public ActionResult Index(int pageNumber = 1)
        {
            if (User.Identity.Name != "")
            {
                var user = db.Users.First(U => U.Email == User.Identity.Name);
                Session["Ad"] = user.FirstName;
                Session["Soyad"] = user.LastName;
                Session["UserName"] = user.UserName.ToString();
                Session["userid"] = user.ID.ToString();
                Session["Mail"] = user.Email.ToString();
                ViewBag.isAuthenticated = true;
            }
            var listedResults = db.Products.ToList();
            return View(listedResults.ToPagedList(pageNumber ,3));
        }
    }
}