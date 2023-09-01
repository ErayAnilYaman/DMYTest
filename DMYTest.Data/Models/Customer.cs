namespace DMYTest.Data.Models
{
    #region Usings
    using System.Collections;
    using System.Collections.Generic;

    #endregion

    public class Customer
    {
        private ICollection<Order> _orders;
        public Customer()
        {
            _orders = new HashSet<Order>();
        }
        public int CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<Order> Orders { get { return _orders; } set { this._orders = value; } }
    }
}