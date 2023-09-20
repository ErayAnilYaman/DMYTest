
namespace DMYTest.Web.Controllers
{
    #region Usings
using DMYTest.Data.Context;
    using DMYTest.Data.Encryption;
    using DMYTest.Data.Models;
    using DMYTest.Web.Models;
using System.Linq;
using System.Web.Mvc;

    #endregion
    public class UserController : Controller
    {
        InternDBContext db = new InternDBContext();
        public ActionResult Update()
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = (string)Session["Mail"];
                var user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);

                return View(user);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Update(UserToUpdate userToUpdate)
        {
            if (User.Identity.IsAuthenticated)
            {
                byte[] passwordHash, passwordSalt;
                var user = db.Users.FirstOrDefault(U => U.Email == User.Identity.Name);
                HashingHelper.HashPassword(userToUpdate.Password , out passwordHash, out passwordSalt);

                user.Email = userToUpdate.Email;
                user.UserName = userToUpdate.UserName;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Status = true;
                
                db.SaveChanges();
                return View();
            }
            return HttpNotFound();
        }
    }
}