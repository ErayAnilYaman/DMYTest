
namespace DMYTest.Data.Concrete
{
    #region Usings
using DMYTest.Data.Abstract;
using DMYTest.Data.Concrete.Base;
using DMYTest.Data.Context;
using DMYTest.Data.Models;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    #endregion
    public  class ProductRepository : EfEntityRepositoryBase<Product> , IProductDal
    {
        
        InternDBContext context = new InternDBContext();
        public List<Product> GetPopularList()
        {
            return context.Products.Where(p => p.Popular == true).Take(3).ToList();
        }

    }
}
