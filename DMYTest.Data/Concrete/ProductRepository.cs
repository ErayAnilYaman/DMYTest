
namespace DMYTest.Data.Concrete
{
    #region Usings
using DMYTest.Data.Abstract;
using DMYTest.Data.Concrete.Base;
using DMYTest.Data.Context;
using DMYTest.Data.Models;
    using System.Collections.Generic;

    #endregion
    public  class ProductRepository : EfEntityRepositoryBase<Product> , IProductDal
    {
        InternDBContext context = new InternDBContext();

        public List<Product> Get
    }
}
