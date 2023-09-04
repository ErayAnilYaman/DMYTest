
namespace DMYTest.Data.Concrete.Base
{
    #region Usings
using DMYTest.Data.Abstract.Base;
using DMYTest.Data.Models.Abstract;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

    #endregion
    public class EfEntityRepositoryBase<TContext, TEntity> : IEntityRepositoryBase<TEntity>
        where TContext : DbContext, new()
        where TEntity : class , IEntity , new()
    {
        DbSet<TEntity> _data;

        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {

                _data.Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                _data.Remove(entity);
                context.SaveChanges();
            }
        }

        public TEntity GetById(int id)
        {
            using (var context = new TContext())
            {
                return _data.Find(id);
            }
        }

        public List<TEntity> List()
        {
            using (var context = new TContext())
            {
                return _data.ToList();

            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
