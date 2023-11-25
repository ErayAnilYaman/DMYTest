namespace DMYTest.Web.Controllers
{
    #region Usings
    using DMYTest.Data.Context;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using PagedList;
    using Microsoft.Ajax.Utilities;
    using System.Web.Services.Description;
    #endregion
    public class AdminController : Controller
    {
        DMYDBContext db = new DMYDBContext();
        [Authorize(Roles="admin")]
        public ActionResult Index(int pageNumber = 1)
        {
            var user = db.Users.FirstOrDefault(U => U.Email == User.Identity.Name);
            
            return View(db.Products.ToList().ToPagedList(pageNumber , 5));
        }
        
        [Authorize(Roles = "admin")]
        public ActionResult Comment(int page = 1)
        {
            return View(db.Comments.ToList().ToPagedList(page, 5));
        }
        [HttpPost]  
        [Authorize(Roles = "admin")]
        public JsonResult DeleteComment(int id)
        {
            // Burada Kaldin
            try
            {
                var commentToDelete = db.Comments.Find(id);
                if (commentToDelete != null)
                {
                    db.Comments.Remove(commentToDelete);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Comment Deleted !" });
                }
                return Json(new { success = false, message = "Comment Not Found" });
            }
            catch (Exception ex)
            {

                return Json(new {  success = false  , message = ex.Message });
                
            }
            
        }

    }
}