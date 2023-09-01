
namespace DMYTest.Data.Models
{
    
    #region Usings
    using System.ComponentModel.DataAnnotations;
    using System;
    #endregion
    public class ProductImage
    {

        
        public int ProductImageID { get; set; }
        public int ProductID { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }
        public virtual Product Product { get; set; }
    }
}