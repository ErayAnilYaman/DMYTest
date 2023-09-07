using DMYTest.Data.Abstract;
using DMYTest.Data.Concrete;
using DMYTest.Data.Models;
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
        CategoryRepository categoryRepository = new CategoryRepository();


        public ActionResult Index()
        {
            return View(categoryRepository.List());
        }
        public ActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Add(category);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Bir Hata Olustu");
            return View();
        }

        
        public ActionResult Delete(int id)
        {
            var deleteEntity = categoryRepository.GetById(id);
            categoryRepository.Delete(deleteEntity);

            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var categoryToUpdate = categoryRepository.GetById(id);
            if (categoryToUpdate == null)
            {
                return HttpNotFound();
            }
            return View(categoryToUpdate);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                var categoryToUpdate = categoryRepository.GetById(category.CategoryID);

                categoryToUpdate.CategoryName = category.CategoryName;
                categoryToUpdate.Description = category.Description;
                categoryRepository.Update(categoryToUpdate);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Bir hata olustu");
            return View(category);
        }

    }
}