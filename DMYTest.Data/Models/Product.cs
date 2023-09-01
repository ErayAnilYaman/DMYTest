


namespace DMYTest.Data.Models
{
    #region Usings
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    #endregion
    public class Product
    {
        private ICollection<Order> _orders;
        private ICollection<ProductImage> _productImages;

        public Product()
        {
            _orders = new HashSet<Order>();
            _productImages = new HashSet<ProductImage>();
        }

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public int UnitPrice { get; set; }

        public int Stock { get; set; }

        public int CategoryID { get; set; }

        public virtual Category Categories { get; set; }
        
        public virtual ICollection<Order> Orders
        {
            get
            {
                return _orders;
            }
            set
            {
                this._orders = value;
            }
        }

        public virtual ICollection<ProductImage> ProductImages
        {
            get 
            { 
                return _productImages; 
            }
            set
            {
                this._productImages = value;
            }
}
    }
}
