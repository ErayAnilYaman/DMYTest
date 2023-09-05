namespace DMYTest.Data.Models
{ 
    #region Usings
    using DMYTest.Data.Models.Abstract;
   

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    #endregion


    public class Category : IEntity
    {
        private ICollection<Product> products;

        public Category()
        {
            products = new HashSet<Product>();
        }
        
        public int CategoryID { get; set; }
        [Required(ErrorMessage ="Lutfen Doldurunuz")]
        [Display(Name ="Kategori Adi")]
        [StringLength(50,ErrorMessage ="karakter sinirini gectiniz")]
        
        public string CategoryName { get; set; }
        [Required(ErrorMessage ="Lutfen Doldurunuz")]
        [Display(Name ="Aciklama")]
        [StringLength(100,ErrorMessage ="Karakter sinirini gectiniz")]
        public string Description { get; set; }

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
