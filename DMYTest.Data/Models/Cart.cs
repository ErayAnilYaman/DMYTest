
namespace DMYTest.Data.Models
{
    using DMYTest.Data.Models.Abstract;
    #region Usings
    using System;
using System.ComponentModel.DataAnnotations;
    #endregion
    public class Cart : IEntity
    {
        public int CartID { get; set; }


        [Required(ErrorMessage = "Lutfen pro Doldurunuz")]
        [Display(Name = "Urun")]
        public int ProductID { get; set; }


        [Required(ErrorMessage = "Lutfen qua Doldurunuz")]
        [Display(Name = "Miktar")]
        public int Quantity { get; set; }


        [Required(ErrorMessage = "Lutfen price Doldurunuz")]
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Lutfen date Doldurunuz")]
        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }


        [Required(ErrorMessage = "Lutfen image Doldurunuz")]
        [Display(Name = "Resim")]
        [StringLength(500, ErrorMessage = "karakter sinirini gectiniz")]
        public string Image { get; set; }


        [Required(ErrorMessage = "Lutfen user Doldurunuz")]
        [Display(Name = "Kullanici")]
        public int UserID { get; set; }


        public virtual Product Product { get; set; }
        public virtual User User { get; set; }

    }
}
