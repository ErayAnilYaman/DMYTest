
namespace DMYTest.Data.Concrete
{
    #region Usings
using DMYTest.Data.Abstract;
using DMYTest.Data.Concrete.Base;
using DMYTest.Data.Context;
using DMYTest.Data.Models;

    #endregion
    public  class ProductRepository : EfEntityRepositoryBase<Product> , IProductDal
    {


    }
}
