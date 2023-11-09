
namespace DMYTest.Data.Models
{
    using DMYTest.Data.Models.Abstract;
    #region Usings
    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    #endregion
    public class Sales : IEntity
    {
        [Key]
        public int SalesID { get; set; }

        [Required(ErrorMessage = "Lutfen p Doldurunuz")]
        [Display(Name = "Urun")]
        public int ProductID { get; set; }


        [Required(ErrorMessage = "Lutfen q Doldurunuz")]
        [Display(Name = "Adet")]
        public int Quantity { get; set; }


        [Required(ErrorMessage = "Lutfen price Doldurunuz")]
        [Display(Name = "Ucret")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }


        [Required(ErrorMessage = "Lutfen image Doldurunuz")]
        [Display(Name = "Maas")]
        [StringLength(150, ErrorMessage = "karakter sinirini gectiniz")]
        public string Image { get; set; }


        [Required(ErrorMessage = "Lutfen user Doldurunuz")]
        [Display(Name = "Kullanici")]
        public int UserID { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
