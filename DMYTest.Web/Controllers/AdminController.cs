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
        InternDBContext db = new InternDBContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Comment(int page = 1)
        {
            return View(db.Comments.ToList().ToPagedList(page , 3));
        }
        public ActionResult Delete(int id)
        {
            var commentToDelete = db.Comments.Where(x => x.ID == id).FirstOrDefault();
            db.Comments.Remove(commentToDelete);
            db.SaveChanges();
            return RedirectToAction("Comment");


        }

    }
}