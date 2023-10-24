namespace DMYTest.Data.Models
{
    
    #region Usings
    using System;using DMYTest.Data.Models.Abstract;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    #endregion

    public class Order : IEntity
    {
        private ICollection<ProductOrderCrossModel> _productOrderCrossModels;
        private ICollection<Sales> _sales;
        public Order()
        {
            _sales = new HashSet<Sales>();
            _productOrderCrossModels = new HashSet<ProductOrderCrossModel>();
        }
        [Key]
        public int OrderID { get; set; }
        

        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Musteri")]
        public int UserID { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Adres")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Telefon Numarasi")]
        [Phone]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Siparis Tarihi")]
        public DateTime OrderDate { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Urun Iletildi mi")]
        public bool OrderReceived { get; set; }

        public virtual ICollection<ProductOrderCrossModel> ProductOrderCrossModels { get {return _productOrderCrossModels; } set { this._productOrderCrossModels = value;  } }
        public virtual ICollection<Sales> Sales { get { return _sales; } set { _sales = value; } }

        public virtual User User { get; set; }


    }
}
