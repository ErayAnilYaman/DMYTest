
namespace DMYTest.Data.Models
{

    #region Usings
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.InteropServices;
    using DMYTest.Data.Models.Abstract;
    #endregion
    public class Product : IEntity
    {
        
        private ICollection<Image> _images;
        private ICollection<Comment> _comments;
        private ICollection<Cart> _carts;
        private ICollection<Sales> _sales;
        private ICollection<ProductOrderCrossModel> _crossModels;
        public Product()
        {
            
            _comments = new HashSet<Comment>();
            _images = new HashSet<Image>();
            _carts = new HashSet<Cart>();
            _sales = new HashSet<Sales>();
            _crossModels = new HashSet<ProductOrderCrossModel>();
        }
        [Key]
        public int ProductID { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Urun Adi")]
        [StringLength(75, ErrorMessage = "karakter sinirini gectiniz" ,MinimumLength =2)]
        public string ProductName { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Urun Aciklamasi")]
        [StringLength(150, ErrorMessage = "karakter sinirini gectiniz" , MinimumLength = 5)]
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
        
        public virtual ICollection<Cart> Carts { get {return _carts; } set {this._carts = value; } }

        public virtual ICollection<Sales> Sales { get { return _sales; } set { this._sales = value; } }

        public virtual ICollection<Image> Images { get { return _images; }set { this._images = value; }  }

        public virtual ICollection<ProductOrderCrossModel> ProductOrderCrossModels { get { return _crossModels; } set { this._crossModels = value; } }
        public virtual ICollection<Comment> Comments { get {return _comments; } set {this._comments = value; } }
    }
}
