
namespace DMYTest.Web.Models
{
    #region Usings
using System;

    #endregion
    public class Sales
    {
        public int SalesID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public int UserID { get; set; }

    }
}