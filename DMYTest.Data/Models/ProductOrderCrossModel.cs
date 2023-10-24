using DMYTest.Data.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMYTest.Data.Models
{
    public class ProductOrderCrossModel :IEntity
    {
        [Key]
        public int CrossID { get; set; }
        public int ProductID { get; set; }
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
