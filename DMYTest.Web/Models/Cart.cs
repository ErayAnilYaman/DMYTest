using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMYTest.Web.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public int UserID { get; set; }

    }
}