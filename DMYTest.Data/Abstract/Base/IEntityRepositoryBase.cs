using DMYTest.Data.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DMYTest.Data.Abstract.Base
{
    public interface IEntityRepositoryBase<T> where T : class ,IEntity , new()
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        T GetById(int id);
        List<T> List();
    }
}
