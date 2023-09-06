﻿using DMYTest.Data.Concrete;
using DMYTest.Data.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMYTest.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        
        ProductRepository productRepository = new ProductRepository();
        public ActionResult Index(int pageNumber = 1)
        {
            return View(productRepository.List().ToPagedList(pageNumber ,3));
        }
    }
}