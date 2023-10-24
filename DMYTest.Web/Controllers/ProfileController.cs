using DMYTest.Data.Concrete;
using DMYTest.Data.Context;
using DMYTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMYTest.Web.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        DMYDBContext db = new DMYDBContext();
        CustomerRepository customerRepository = new CustomerRepository();
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.FirstOrDefault(U=>U.Email == User.Identity.Name);
                if (user !=null)
                {
                    return View(user);
                }
                return HttpNotFound();
            }
            ModelState.AddModelError("", "Kullanici Bulunamadi");
            return View(ModelState);
        }
        public ActionResult Create()
        {
            var user = db.Users.First(U => U.Email == User.Identity.Name);
            if (user.Customer != null)
            {
                return View("index");
            }
            if (User.Identity.IsAuthenticated )
            {
                
                return View();
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Create(Customer c)
        {
            if (User.Identity.IsAuthenticated )
            {
                var user = db.Users.First(U => U.Email == User.Identity.Name);
                if (ModelState.IsValid && user.Customer == null)
                {
                    var customer = new Customer
                    {
                        User = user,
                        Email = c.Email,
                        Address = c.Address,
                        CompanyName = c.CompanyName,
                        Phone = c.Phone,
                        Country = c.Country,
                        PostCode = c.PostCode,

                    };

                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("index","profile");
                }
                ModelState.AddModelError("", "Bilgilerinizi hatali girdiniz .Lutfen kurallara uygun giriniz");
                return View();
            }
            return View();
        }
        #region Update
        public ActionResult Update()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.First(U => U.Email == User.Identity.Name);
                if (user.Customer != null)
                {
                    ViewBag.CustomerID = user.Customer.CustomerID;

                    return View(user.Customer);
                }
                return View("index");
            }
            return RedirectToAction("login", "account");

        }
        [HttpPost]
        public ActionResult Update(Customer c)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    //var customerToUpdate = db.Customers.Find(c.CustomerID);
                    var user = db.Users.First(U => U.Email == User.Identity.Name);

                    user.Customer.Address = c.Address;
                    user.Customer.Country = c.Country;
                    user.Customer.CustomerID = c.CustomerID;
                    user.Customer.CompanyName = c.CompanyName;
                    user.Customer.Phone = c.Phone;
                    user.Customer.Email = c.Email;
                    user.Customer.PostCode = c.PostCode;
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("", "Bilgileriniz guncellenirken hata olustu!");
                return View("index");
            }
            return RedirectToAction("login", "account");

        }
        #endregion

    }
}