

namespace DMYTest.Data.Models
{

    #region Usings
using DMYTest.Data.Models.Abstract;
using System.ComponentModel.DataAnnotations;

    #endregion
    public class Employee : IEntity
    {
        public int EmployeeID { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Kullanici")]
        public int UserID { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Calisan Adi")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string EmployeeName { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Is")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string Job { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Maas")]
        public decimal Salary { get; set; }


        public virtual User User { get; set; }
    }
}
