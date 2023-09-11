using DMYTest.Data.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMYTest.Web.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository repository =new CategoryRepository();
        // GET: Category
        public PartialViewResult CategoryList()
        {
            return PartialView(repository.List());
        }

        public ActionResult Details(int id)
        {
            var category = repository.CategoryDetails(id);
            return View(category);
        }
    }
}