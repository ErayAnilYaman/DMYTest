using DMYTest.Data.Concrete;

namespace DMYTest.Web.Controllers
{
    
    #region Usings
    using PagedList;
    using System.Linq;
    using System.Web.Mvc;
    using DMYTest.Data.Context;
    using DMYTest.Data.Models;
    using System.Collections.Generic;
    using System.Web;
    using System.IO;
    using System;

    #endregion
    public class AdminProductImageController : Controller
    {
        // GET: AdminProductImage
        ProductImageRepository imageRepository = new ProductImageRepository();
        InternDBContext context = new InternDBContext();
        public ActionResult Index()
        {
            return View(imageRepository.List().ToPagedList(pageNumber:1 ,pageSize :3));
        }

        public ActionResult Create()
        {
            List<SelectListItem> value = (from i in context.Products.ToList()
                         
                         select new SelectListItem
                         {
                             Text  = i.ProductName, 
                             Value = i.ProductID.ToString()
                         }
                         ).ToList();
            
            ViewBag.prdct = value;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductImage image , HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("", errorMessage: "Bir hata olustu");
                return View(image);
                
            }
            var result = CheckIfImageAlreadyExists(image.ProductID);
            if (!result)
            {
                ModelState.AddModelError("", "Urune ait gecerli bir resim zaten mevcut dilerseniz urune guncelleme yapabilirsiniz");
                return View(image);
            }
            
            string path = Path.Combine("~/Content/Image/" + file.FileName);
            file.SaveAs(Server.MapPath(path));
            image.ImagePath = file.FileName.ToString();
            image.Date = DateTime.Now;
            imageRepository.Add(image);
            return RedirectToAction("Index");
           
        }

        private bool CheckIfImageAlreadyExists(int id)
        {
            var imageToCheck = context.Products.Find(id);
            if (imageToCheck.ProductID == 0  && imageToCheck.ProductID == null)
            {
                return false;
            }
            return true;
        }
    }
}