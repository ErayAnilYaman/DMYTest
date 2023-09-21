
namespace DMYTest.Data.Concrete
{
    #region usings
    using DMYTest.Data.Abstract;
    using DMYTest.Data.Concrete.Base;
    using DMYTest.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion
    public class OrderRepository : EfEntityRepositoryBase<Order>, IOrderDal
    {

    }
}
