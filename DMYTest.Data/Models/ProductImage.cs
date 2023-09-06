
namespace DMYTest.Data.Models
{
    
    #region Usings
    using System.ComponentModel.DataAnnotations;
    using System;
    using DMYTest.Data.Models.Abstract;
    #endregion
    public class ProductImage : IEntity
    {

        
        public int ProductImageID { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Urun")]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Resim")]
        [StringLength(150, ErrorMessage = "karakter sinirini gectiniz")]
        public string ImagePath { get; set; }
        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }
        public virtual Product Product { get; set; }
    }
}