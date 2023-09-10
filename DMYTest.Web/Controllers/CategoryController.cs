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
        CategoryRepository respository =new CategoryRepository();
        // GET: Category
        public PartialViewResult CategoryList()
        {
            return PartialView(respository.List());
        }
    }
}