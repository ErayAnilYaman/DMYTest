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
    using System.Web;
    using System.IO;
    using System;
    #endregion
    public class AdminProductController : Controller
    {

        ProductRepository productRepository = new ProductRepository();
        ImageRepository imageRepository = new ImageRepository();
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
        public ActionResult Create(Product product , HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine("~/Content/Image/" + file.FileName);
                file.SaveAs(Server.MapPath(path));
                product.Images.Add(new Image
                {
                    Date = DateTime.Now,
                    ImagePath = file.FileName.ToString(),
                    ProductID = product.ProductID,
                    Product = product

                });
                ViewBag.img = file.FileName.ToString();
                if (!CheckIfImageCountMax(product.ProductID))
                {
                    ModelState.AddModelError("", "Resim sayi siniri 5 tir bu sinir gecilemez");
                    return View();
                }
                product.Date = DateTime.Now;
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
            ViewBag.ProductID = id;
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
        public ActionResult ImageUpdate(int productId)
        {
            var productToUpdate = productRepository.GetById(productId);
            var value = (from i in context.Images.Where(i => i.ProductID == productId).ToList()
                         select new SelectListItem {
                             Value = i.ImageID.ToString(),
                             Text = i.ImagePath
                         });
            ViewBag.img = value;
            ViewBag.ProductID = productId;
            if (productToUpdate == null)
            {
                ModelState.AddModelError("", "Bir hata olustu");
                return HttpNotFound();
            }
            return View(productToUpdate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImageUpdate(Image image)
        {
            image.Date = DateTime.Now;
            string[] imagePathExtension = image.ImagePath.Split('.');
            if (imagePathExtension[1] != "png" || imagePathExtension[1] != "jpeg")
            {
                ModelState.AddModelError("", "Resim .png veya .jpeg dosya uzantisinda olmali");
                return View();
            }
            if (!ModelState.IsValid)
            {

                ModelState.AddModelError("","Resmi eksiksiz bicimde girdiginize emin misiniz");
                return View();
            }
            imageRepository.Add(image);
            return RedirectToAction("imageupdate");

        }

        private bool CheckIfImageCountMax(int productId)
        {
            var countOfImageProduct = productRepository.List().Where(p => p.ProductID == productId).Count();
            if (countOfImageProduct < 5)
            {
                return true;
            }
            return false;
        }
    }
}