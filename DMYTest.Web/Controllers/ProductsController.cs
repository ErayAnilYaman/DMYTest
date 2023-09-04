using DMYTest.Data.Context;
using DMYTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace DMYTest.Web.Controllers
{
    
    public class ProductsController : Controller
    {
        // GET: Products
        
        public ActionResult Index()
        {
            
            using (var context = new InternDBContext())
            {
                var listedProduct = context.Products.OrderBy(a => a.UnitPrice);
                return this.View(listedProduct.ToList());

            }

        }
        public ActionResult Edit(Product product)
        {
            using (var context = new InternDBContext())
            {
                if (ModelState.IsValid)
                {
                    context.Entry(product).State = EntityState.Modified;

                    return this.RedirectToAction("Index");
                }
                else
                {
                    this.ViewBag.CategoryID = new SelectList
                    (
                        context.Categories , "CategoryID" ,"CategoryName" ,product.CategoryID
                    );
                    return View(product);
                }
                
                
            }
        }
    }
}