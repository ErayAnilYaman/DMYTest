using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DMYTest.Web.Models
{
    public class UserToLogin
    {
        [Required(ErrorMessage = " Mail zorunludur")]
        [Display(Name = "E-Mail Adresi")]
        [StringLength(100, ErrorMessage = "karakter sinirini gectiniz")]
        public string Email { get; set; }


        [Required(ErrorMessage = " Sifre zorunludur")]
        [Display(Name = "Sifre")]
        [StringLength(250, ErrorMessage = "karakter sinirini gectiniz")]
        public string Password { get; set; }


    }
}