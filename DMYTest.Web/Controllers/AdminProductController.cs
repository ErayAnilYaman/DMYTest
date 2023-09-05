namespace DMYTest.Web.Controllers
{
    #region Usings
    using DMYTest.Data.Concrete;
    using DMYTest.Data.Context;
    using DMYTest.Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    #endregion
    public class AdminProductController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        InternDBContext context = new InternDBContext();
        // GET: AdminProduct
        public ActionResult Index()
        {
            return View(productRepository.List());
        }

        public ActionResult Create()
        {
            List<SelectListItem> value = (from i in context.Categories.ToList() 
            select new SelectListItem
            {
                Text = i.CategoryName,
                Value = i.CategoryID.ToString()
            }).ToList();
            ViewBag.ctgr = value;
            return View(value);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.Add(product);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Bir Hata Olustu");
            return View(product);

        }

    }
}