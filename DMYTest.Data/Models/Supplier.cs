
namespace DMYTest.Data.Models
{
    #region Usings

using DMYTest.Data.Models.Abstract;
using System.ComponentModel.DataAnnotations;

    #endregion
    public class Supplier : IEntity
    {

        public int SupplierID { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Kullanici")]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Sirket Adi")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Iletisim Adi")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Iletisim Basligi")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string ContactTitle { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Adres")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string Address { get; set; }
        public virtual User User { get; set; }
    }
}
