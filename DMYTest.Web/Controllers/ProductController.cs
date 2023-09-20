namespace DMYTest.Web.Controllers
{
    #region Usings
    using DMYTest.Data.Concrete;
    using DMYTest.Data.Context;
    using DMYTest.Data.Models;
    using System;
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
        public JsonResult AddComment(int productID ,string content)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = context.Users.FirstOrDefault(U=>U.Email == User.Identity.Name);
                var comment = new Comment
                {
                    Contents = content,
                    Date = DateTime.Now,
                    ProductID = productID,
                    UserID = user.ID,

                };
                context.Comments.Add(comment);
                context.SaveChanges();
                return Json(new { success = true, message = "Yorumunuz Eklendi", productID = comment.ProductID, content = comment.Contents, username = user.UserName, date = DateTime.Now.ToString(), commentID = comment.ID});
            }
            return Json(new { success = false, message = "Yorum yapabilmek icin giris yapmalisiniz!" });
            
        }
    }
}
