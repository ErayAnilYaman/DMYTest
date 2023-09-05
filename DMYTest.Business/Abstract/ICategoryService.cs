using DMYTest.Data.Models;
using System.Collections.Generic;
namespace DMYTest.Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> List();
        void Delete(int id);

    }
}
