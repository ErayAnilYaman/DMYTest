
namespace DMYTest.Web.Models
{

    #region Usings
using System;
using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
using System.Web;

    #endregion
    public class UserToUpdate
    {
        [Required(ErrorMessage = "Ad zorunludur")]
        [Display(Name = "Ad")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Soyad zorunludur")]
        [Display(Name = "Soyad")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string LastName { get; set; }


        [Required(ErrorMessage = " Mail zorunludur")]
        [Display(Name = "E-Mail Adresi")]
        [StringLength(100, ErrorMessage = "karakter sinirini gectiniz")]
        public string Email { get; set; }


        [Required(ErrorMessage = " Sifre zorunludur")]
        [Display(Name = "Sifre")]
        [StringLength(250, ErrorMessage = "karakter sinirini gectiniz")]
        public string Password { get; set; }


        [Required(ErrorMessage = " Sifre Tekrari zorunludur")]
        [Display(Name = "Sirket Adi")]
        [StringLength(250, ErrorMessage = "karakter sinirini gectiniz")]
        public string RePassword { get; set; }


        [Required(ErrorMessage = " Kullanici Adi zorunludur")]
        [Display(Name = "Kullanici Adi")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string UserName { get; set; }
    }
}