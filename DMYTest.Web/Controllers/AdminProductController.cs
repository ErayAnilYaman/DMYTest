namespace DMYTest.Web.Controllers
{
    #region Usings
    using DMYTest.Data.Concrete;
    using DMYTest.Data.Context;
    using DMYTest.Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using PagedList;
    using PagedList.Mvc;
    #endregion
    public class AdminProductController : Controller
    {

        ProductRepository productRepository = new ProductRepository();
        InternDBContext context = new InternDBContext();
        // GET: AdminProduct
        public ActionResult Index(int pageNumber=1)
        { 
            return View(productRepository.List().ToPagedList(pageNumber , 3));
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
            return View();

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
        
        public ActionResult Delete(int id)
        {
            var productToDelete = productRepository.GetById(id);
            productRepository.Delete(productToDelete);
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var productToUpdate = productRepository.GetById(id);
            var value = (from i in context.Categories.ToList()
                         select new SelectListItem
                         {
                             Text = i.CategoryName,
                             Value = i.CategoryID.ToString()
                         }).ToList();
            ViewBag.ctgr = value;
            ViewBag.Price = productToUpdate.UnitPrice;
            if (productToUpdate == null)
            {
                ModelState.AddModelError("", "Bir hata olustu");
                return HttpNotFound();
            }
            return View(productToUpdate);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Product product)
        {
            var productToUpdate = productRepository.GetById(product.ProductID);
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Urun Bilgileri Urun kosullarini karsilamiyor");
                return View(productToUpdate);
            }

            productToUpdate.ProductName = product.ProductName;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.Stock = product.Stock;
            productToUpdate.Description = product.Description;
            productToUpdate.CategoryID = product.CategoryID;
            
            productRepository.Update(productToUpdate);
            return RedirectToAction("Index");
        }

    }
}