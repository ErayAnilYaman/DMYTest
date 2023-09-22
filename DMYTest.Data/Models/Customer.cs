namespace DMYTest.Data.Models
{
    using DMYTest.Data.Models.Abstract;
    #region Usings
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public class Customer : IEntity
    {


        private User _user;
        public Customer()
        {
            _user = new User();
        }

        [Key]

        [ForeignKey("User")]
        public int UserID { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Sirket Adi")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string CompanyName { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Adres Adi")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Lutfen Email Adres kismini doldurunuz!")]
        [StringLength(75, ErrorMessage = "Karakter sinirini gectiniz maksimum 75 karakter girebilirsiniz!")]
        [Display(Name = "Ofis Mail Adresi")]
        [EmailAddress]
        public string OfficeEmailAddress { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Telefon Numarasi")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        [Phone]
        public string Phone { get; set; }

        public virtual User User { get; set; }

    }
}