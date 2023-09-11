namespace DMYTest.Web.Controllers
{
    #region Usings
    using DMYTest.Data.Concrete;
    using DMYTest.Data.Context;
    using DMYTest.Data.Models;
    using System.Linq;
    using System.Web.Mvc;
    #endregion
    public class ProductController : Controller
    {
        ProductRepository repository = new ProductRepository();
        InternDBContext context = new InternDBContext();
        // GET: Product
        public PartialViewResult Popular(int pageNumber = 1)
        {
            var product = repository.GetPopularList();
            ViewBag.popular = product;
            return PartialView(product);
        }
        public ActionResult ProductDetails(int id)
        {
            var details = repository.GetById(id);
            var comment = context.Comments.Where(c => c.ProductID == id).ToList();
            ViewBag.Comment = comment;
            return View(details);
        }
        [HttpPost]
        public ActionResult Comment(Comment comment)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    context.Comments.Add(comment);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Yorumu Eksiksiz bir sekilde giriniz");
                return View();
            }
            ModelState.AddModelError("","Once uye olmaniz veya oturum acmaniz gerekiyor.");
            return RedirectToAction("index","home");
        }
    }
}
