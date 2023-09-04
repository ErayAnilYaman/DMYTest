
namespace DMYTest.Data.Models
{
    #region Usings
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    #endregion
    public class Sales
    {
        public int SalesID { get; set; }

        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Urun")]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Adet")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Ucret")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Maas")]
        [StringLength(150, ErrorMessage = "karakter sinirini gectiniz")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Kullanici")]
        public int UserID { get; set; }


        public virtual User User { get; set; }
    }
}
