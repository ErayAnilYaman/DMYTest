
namespace DMYTest.Data.Concrete
{
    #region Usings
using DMYTest.Data.Abstract;
using DMYTest.Data.Concrete.Base;
using DMYTest.Data.Models;

    #endregion
    public class SaleRepository : EfEntityRepositoryBase<Sales> , ISaleDal
    {

    }
}
