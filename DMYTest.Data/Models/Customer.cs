namespace DMYTest.Data.Models
{
    using DMYTest.Data.Models.Abstract;
    #region Usings
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class Customer : IEntity
    {
        
        
        public int CustomerID { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Sirket Adi")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string CompanyName { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Adres Adi")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Telefon Numarasi")]
        [StringLength(50, ErrorMessage = "karakter sinirini gectiniz")]
        public string Phone { get; set; }


    }
}