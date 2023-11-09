using DMYTest.Data.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMYTest.Data.Models
{
    public class CommentLikeModel  : IEntity
    {
        [Key]
        public int LikeID { get; set; }
        
        public virtual Comment Comment { get; set; }
        public virtual User User { get; set; }
    }
}
