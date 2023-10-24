
namespace DMYTest.Web.Controllers
{
    #region MyRegion
using DMYTest.Data.Concrete;
using DMYTest.Data.Context;
using DMYTest.Data.Models;
using System.Web.Mvc;

    #endregion
    public class AdminCategoryController : Controller
    {
        // GET: AdminCategory
        CategoryRepository categoryRepository = new CategoryRepository();
        DMYDBContext db = new DMYDBContext();

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(categoryRepository.List());
        }


        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }


        [Authorize(Roles = "admin")]
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


        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var deleteEntity = categoryRepository.GetById(id);
            categoryRepository.Delete(deleteEntity);

            return RedirectToAction("Index");
        }

        #region Update
        [Authorize(Roles = "admin")]
        public ActionResult Update(int id)
        {
            var categoryToUpdate = categoryRepository.GetById(id);
            if (categoryToUpdate == null)
            {
                return HttpNotFound();
            }
            return View(categoryToUpdate);
        }


        [Authorize(Roles = "admin")]
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
        #endregion


    }
}