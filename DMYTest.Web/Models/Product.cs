﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMYTest.Web.Models
{
    public class Product
    {

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime Date { get; set; }
        public int Stock { get; set; }
        public int CategoryID { get; set; }
        public bool Popular { get; set; }


    }
}