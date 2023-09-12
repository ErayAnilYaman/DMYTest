


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
        private ICollection<Comment> _comments;
        private ICollection<Cart> _carts;
        public Product()
        {
            _orders = new HashSet<Order>();
            _comments = new HashSet<Comment>();
            _images = new HashSet<Image>();
            _carts = new HashSet<Cart>();
        }

        public int ProductID { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Urun Adi")]
        [StringLength(75, ErrorMessage = "karakter sinirini gectiniz")]
        public string ProductName { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Urun Aciklamasi")]
        [StringLength(150, ErrorMessage = "karakter sinirini gectiniz")]
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


        [Required(ErrorMessage ="Lutfen bos birakmayiniz")]
        [Display(Name ="Populer mi")]
        public bool Popular { get; set; }



        public virtual Category Category { get; set; }
        
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
        public virtual ICollection<Cart> Carts { get {return _carts; } set {this._carts = value; } }
        public virtual ICollection<Image> Images { get { return _images; }set { this._images = value; }  }
        public virtual ICollection<Comment> Comments { get {return _comments; } set {this._comments = value; } }
    }
}
