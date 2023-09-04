using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMYTest.Data.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Urun")]
        public int ProductID { get; set; }

        [Display(Name = "Miktar")]
        public int Quantity { get; set; }

        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }

        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }

        [Display(Name = "Resim")]
        public string Image { get; set; }

        [Display(Name = "Kullanici")]
        public int UserID { get; set; }



        public virtual User User { get; set; }

    }
}
