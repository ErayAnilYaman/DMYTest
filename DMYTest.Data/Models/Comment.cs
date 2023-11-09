using DMYTest.Data.Context;
using DMYTest.Data.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMYTest.Data.Models
{
    public class Comment : IEntity
    {
        private ICollection<CommentLikeModel> _likes;
        public Comment()
        {
            _likes = new HashSet<CommentLikeModel>();
        }
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Lutfen Doldurunuz")]
        [Display(Name = "Icerik")]
        [StringLength(300, ErrorMessage = "karakter sinirini gectiniz")]
        public string Contents { get; set; }

        
        [Display(Name = "Urun ID")]
        public int ProductID { get; set; }
        
        [Display(Name = "Kullanici ID")]
        public int UserID { get; set; }
        
        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CommentLikeModel> Likes { get { return this._likes; } set {value = this._likes; } }

    }
}
