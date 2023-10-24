
namespace DMYTest.Data.Concrete
{
    #region Usings
using DMYTest.Data.Abstract;
using DMYTest.Data.Concrete.Base;
using DMYTest.Data.Context;
using DMYTest.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    #endregion
    public class CategoryRepository  :EfEntityRepositoryBase<Category>, ICategoryDal
    {
        DMYDBContext context = new DMYDBContext();
        public List<Product> CategoryDetails(int id)
        {
            return context.Products.Where(p => p.CategoryID == id).ToList();
        }
    }
}
