using DMYTest.Business.Abstract;
using DMYTest.Data.Abstract;
using DMYTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMYTest.Business.Manager
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void Delete(int id)
        {
            var categoryToDelete = _categoryDal.GetById(id);
            _categoryDal.Delete(categoryToDelete);
        }

        public List<Category> List()
        {
            return _categoryDal.List();
        }
    }
}
