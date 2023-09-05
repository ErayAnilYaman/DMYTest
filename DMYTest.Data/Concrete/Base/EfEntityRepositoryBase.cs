
namespace DMYTest.Data.Concrete.Base
{
    #region Usings
using DMYTest.Data.Abstract.Base;
    using DMYTest.Data.Context;
    using DMYTest.Data.Models.Abstract;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

    #endregion
    public class EfEntityRepositoryBase<TEntity> : IEntityRepositoryBase<TEntity>
        where TEntity : class , IEntity , new()
    {
        DbSet<TEntity> _data;
        InternDBContext context = new InternDBContext();
        public EfEntityRepositoryBase()
        {
            _data = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _data.Add(entity);
            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {

            _data.Remove(entity);
            context.SaveChanges();
        }

        public TEntity GetById(int id)
        {
            return _data.Find(id);
        }

        public List<TEntity> List()
        {
            return _data.ToList();
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
