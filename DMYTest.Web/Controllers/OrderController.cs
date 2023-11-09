using DMYTest.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMYTest.Web.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        DMYDBContext db = new DMYDBContext();
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return HttpNotFound();
        }
        
        

    }
}