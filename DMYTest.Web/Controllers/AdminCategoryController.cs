using DMYTest.Data.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMYTest.Web.Controllers
{
    public class AdminCategoryController : Controller
    {
        // GET: AdminCategory
        CategoryRepository repository = new CategoryRepository();   
        public ActionResult Index()
        {
            return View(repository.List());
        }

    }
}