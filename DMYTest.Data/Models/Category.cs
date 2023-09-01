namespace DMYTest.Data.Models
{
    #region Usings

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    #endregion


    public class Category
    {
        private ICollection<Product> products;

        public Category()
        {
            products = new HashSet<Product>();
        }
        
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products
        {
            get
            {
                return products;
            }

            set
            {
                products = value;
            }
        }
    }
}
