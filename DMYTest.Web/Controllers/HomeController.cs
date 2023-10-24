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
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.IsAuthenticated = "true";
            }
            var listedResults = db.Products.ToList();
            return View(listedResults.ToPagedList(pageNumber ,3));
        }
    }
}