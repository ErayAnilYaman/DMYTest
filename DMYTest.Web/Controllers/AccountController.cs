
namespace DMYTest.Web.Controllers
{
    #region Usings
using DMYTest.Data.Context;
using DMYTest.Data.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

    #endregion
    public class AccountController : Controller
    {
        // GET: Account
        InternDBContext context = new InternDBContext();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var informations = context.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (informations != null)
            {
                FormsAuthentication.SetAuthCookie(informations.Email, false);
                Session["Mail"] = informations.Email.ToString();
                Session["Ad"] = informations.FirstName.ToString();
                Session["Soyad"] = informations.LastName.ToString();
                Session["UserName"] = informations.UserName.ToString();
                Session["userid"] = informations.ID.ToString();
                return RedirectToActionPermanent("index", "home");

            }
            ViewBag.Error = "Mail veya sifreniz hatali";

            return View(user);
        }
        [HttpPost]
        public ActionResult Register(User data)
        {
            data.Role = "user";
            data.Status = true;
            var errors = ModelState.Values.SelectMany(v=>v.Errors);

            if (ModelState.IsValid)
            {
                context.Users.Add(data);
                context.SaveChanges();
                return RedirectToActionPermanent("login");
            }
            ModelState.AddModelError("", "Lutfen bilgileri kuralina gore giriniz");
            
            return View("Login",data);
        }


        
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login","account");
        }
    }
}