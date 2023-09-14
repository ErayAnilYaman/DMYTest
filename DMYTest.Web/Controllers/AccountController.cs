
namespace DMYTest.Web.Controllers
{
    #region Usings
using DMYTest.Data.Context;
    using DMYTest.Data.Encryption;
    using DMYTest.Data.Models;
    using DMYTest.Web.Models;
    using System;
    using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

    #endregion
    public class AccountController : Controller
    {
        // GET: Account
        InternDBContext db = new InternDBContext();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserToLogin userToLogin)
        {
            
            var user = db.Users.FirstOrDefault(U => U.Email == userToLogin.Email);

            if (!CheckUserExists(user, out string message))
            {
                ModelState.AddModelError("", message);
                return View("register","account");
            }
            if (HashingHelper.VerifyPassword(userToLogin.Password, user.PasswordHash , user.PasswordSalt))
            {
                
                 FormsAuthentication.SetAuthCookie(user.Email, false);
                 Session["Mail"] = user.Email.ToString();
                 Session["Ad"] = user.FirstName.ToString();
                 Session["Soyad"] = user.LastName.ToString();
                 Session["UserName"] = user.UserName.ToString();
                 Session["userid"] = user.ID.ToString();
                 return RedirectToActionPermanent("index", "home");

            }
            ModelState.AddModelError("", "Parola Hatasi");
            return View("login");
        }

        private bool CheckUserExists(User user, out string message)
        {
            if (user != null)
            {
                message = "";
                return true;    
            }
            message = "Kullanici Bulunamadi";
            return false;
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserToRegister userToRegister)
        {
            byte[] passwordHash, passwordSalt;

            HashingHelper.HashPassword(userToRegister.Password, out passwordHash, out passwordSalt);

            var data = new User
            {
                Role = "user",
                FirstName = userToRegister.FirstName,
                LastName = userToRegister.LastName,
                Email = userToRegister.Email,
                UserName = userToRegister.UserName,
                Status = true,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };


            if (ModelState.IsValid)
            {
                db.Users.Add(data);
                db.SaveChanges();
                return RedirectToActionPermanent("login");
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            foreach (var error in errors.ToList())
            {
                ModelState.AddModelError("", error.ErrorMessage);

            }

            return View("index","home");
        }


        
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login","account");
        }
    }
}