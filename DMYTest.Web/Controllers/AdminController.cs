using DMYTest.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace DMYTest.Web.Controllers
{
    public class AdminController : Controller
    {
        DMYDBContext db = new DMYDBContext();
        [Authorize(Roles="admin")]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "admin")]
        public ActionResult Comment(int page = 1)
        {
            return View(db.Comments.ToList().ToPagedList(page, 5));
        }


        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var commentToDelete = db.Comments.Where(x => x.ID == id).FirstOrDefault();
            db.Comments.Remove(commentToDelete);
            db.SaveChanges();
            return RedirectToAction("Comment");


        }

    }
}