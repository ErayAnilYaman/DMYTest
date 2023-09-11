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
    using DMYTest.Web.Constants;
    using System.Data.Entity;
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
            List<SelectListItem> items;
            items = (from i in context.Categories.ToList() 
            select new SelectListItem
            {
                Text = i.CategoryName,
                Value = i.CategoryID.ToString()
            }).ToList();

            ViewBag.ctgr = items;
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
            if (productToUpdate == null)
            {
                ModelState.AddModelError("", "Bir hata olustu");
                return HttpNotFound();
            }
            return View(productToUpdate);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Product product , HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var productToUpdate = productRepository.GetById(product.ProductID);
                string path = Path.Combine("~/Content/Image/" + file.FileName);
                file.SaveAs(Server.MapPath(path));
                
                var imageToUpdate = context.Images.SingleOrDefault(i => i.ProductID == product.ProductID);

                context.Images.Remove(imageToUpdate);
                context.SaveChanges();

                imageToUpdate.Date = DateTime.Now;
                imageToUpdate.ImagePath = file.FileName.ToString();
                imageToUpdate.ProductID = product.ProductID;
                
                context.Images.Add(imageToUpdate);
                context.SaveChanges();

                ViewBag.img = file.FileName.ToString();
                if (!CheckIfImageCountMax(product.ProductID))
                {
                    ModelState.AddModelError("", "Resim sayi siniri 5 tir bu sinir gecilemez");
                    return View();
                }
                
                productToUpdate.Date = DateTime.Now;
                productToUpdate.ProductName = product.ProductName;
                productToUpdate.Stock = product.Stock;
                productToUpdate.UnitPrice = product.UnitPrice;
                productToUpdate.CategoryID = product.CategoryID;
                productToUpdate.Description = product.Description;
                
                productRepository.Update(productToUpdate);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Bir Hata Olustu");
            return View(product);
        }
        
        public ActionResult ImageUpdate(int id)
        {
            var productToUpdate = productRepository.GetById(id);
            var value = (from i in context.Images.Where(i => i.ProductID == id).ToList()
                         select new SelectListItem {
                             Value = i.ImageID.ToString(),
                             Text = i.ImagePath
                         });
            ViewBag.img = value;
            ViewBag.ProductID = id;
            if (productToUpdate == null)
            {
                ModelState.AddModelError("", "Bir hata olustu");
                return HttpNotFound();
            }
            return View(imageRepository.List().SingleOrDefault(i=>i.ProductID == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImageUpdate(Image image,HttpPostedFileBase file)
        {
            image.Date = DateTime.Now;
            ViewBag.img = file.FileName.ToString();
            string path = Path.Combine("~/Content/Image/" + file.FileName);
            file.SaveAs(Server.MapPath(path));
            image.ImagePath = file.FileName.ToString();

            image.ProductID = ViewBag.ProductID;


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
            imageRepository.Update(image);
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