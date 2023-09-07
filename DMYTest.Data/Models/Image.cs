using DMYTest.Data.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMYTest.Data.Models
{
    public class Image : IEntity
    {
        public int ImageID { get; set; }
        public int ProductID { get; set; }
        public string ImagePath { get; set; }
        
        public DateTime Date { get; set; }
        public virtual Product Product { get; set; }
    }
}
