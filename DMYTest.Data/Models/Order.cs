namespace DMYTest.Data.Models
{
    
    #region Usings
    using System;using DMYTest.Data.Models.Abstract;
    using System.ComponentModel.DataAnnotations;
    #endregion

    public class Order : IEntity
    {

        public int OrderID { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Urun")]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Musteri")]
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Adres")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Siparis Tarihi")]
        public DateTime OrderDate { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Urun Iletildi mi")]
        public bool OrderReceived { get; set; }

        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
