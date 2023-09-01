namespace DMYTest.Data.Models
{
    #region Usings
    using System;
    #endregion
    
    public class Order
    {

        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int CustomerID { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public bool OrderReceived { get; set; }
        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
