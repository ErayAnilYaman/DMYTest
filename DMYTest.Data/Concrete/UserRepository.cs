
namespace DMYTest.Data.Concrete
{
    #region Usings
    using DMYTest.Data.Abstract;
    using DMYTest.Data.Concrete.Base;
    using DMYTest.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion
    public class UserRepository : EfEntityRepositoryBase<User>, IUserDal
    {

    }
}
