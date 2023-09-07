


namespace DMYTest.Data.Models
{
   

    #region Usings
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DMYTest.Data.Models.Abstract;
    #endregion
    public class Product : IEntity
    {
        private ICollection<Order> _orders;
        private ICollection<Image> _images;
        public Product()
        {
            _orders = new HashSet<Order>();
            _images = new HashSet<Image>();
        }

        public int ProductID { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Urun Adi")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string ProductName { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Urun Aciklamasi")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Urun Fiyati")]
        public decimal UnitPrice { get; set; }

        
        [Display(Name ="Tarih")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Stok Adedi")]
        public int Stock { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Kategori ID si")]
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

        public virtual ICollection<Image> Images { get { return _images; }set { this._images = value; }  }
    }
}
