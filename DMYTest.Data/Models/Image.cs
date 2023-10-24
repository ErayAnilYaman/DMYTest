using DMYTest.Data.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMYTest.Data.Models
{
    public class Image : IEntity
    {
        [Key]
        public int ImageID { get; set; }


        [Display(Name = "Urun ID")]
        public int ProductID { get; set; }


        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Urun Resmi")]
        [StringLength(150, ErrorMessage = "karakter sinirini gectiniz")]
        public string ImagePath { get; set; }


        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }


        public virtual Product Product { get; set; }
    }
}
