﻿
namespace DMYTest.Data.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public int UserID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public virtual User User { get; set; }
    }
}
