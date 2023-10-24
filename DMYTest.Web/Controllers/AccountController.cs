
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
        DMYDBContext db = new DMYDBContext();

        #region Authorization/Authentication

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Obsolete]
        public ActionResult Login(UserToLogin userToLogin)
        {

            var user = db.Users.FirstOrDefault(U => U.Email == userToLogin.Email);

            if (!CheckUserExists(user, out string message))
            {
                ModelState.AddModelError("", message);
                return View("register", "account");
            }
            if (HashingHelper.VerifyPassword(userToLogin.Password, user.PasswordHash, user.PasswordSalt))
            {

                //if(true)
                //{
                //    FormsAuthentication.SetAuthCookie(user.Email, false);
                //    var authenticationType = User.Identity.AuthenticationType;
                //    User.IsInRole(authenticationType);
                //    var value = FormsAuthentication.Authenticate(user.UserName, userToLogin.Password);
                //    var awesome = FormsAuthentication.Decrypt(userToLogin.Password);
                //    var awesomee = FormsAuthentication.Encrypt(awesome);
                //}
                FormsAuthentication.SetAuthCookie(user.Email, false);
                var cookie = FormsAuthentication.GetAuthCookie(user.Email, false);

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
                var user = db.Users.First(U => U.Email == data.Email);

                FormsAuthentication.SetAuthCookie(user.Email, false);
                var cookie = FormsAuthentication.GetAuthCookie(user.Email, false);

                Session["Mail"] = user.Email.ToString();
                Session["Ad"] = user.FirstName.ToString();
                Session["Soyad"] = user.LastName.ToString();
                Session["UserName"] = user.UserName.ToString();
                Session["userid"] = user.ID.ToString();

                return RedirectToAction("index","home");
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            foreach (var error in errors.ToList())
            {
                ModelState.AddModelError("", error.ErrorMessage);

            }

            return View("index", "home");
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login", "account");
        }
        #endregion


        #region privateMethods
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
        #endregion

    }
}