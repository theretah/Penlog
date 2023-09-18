using System.Linq.Expressions;

namespace Penlog.Data.Repository.IRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // 1 - Finding objects
        TEntity Get(int id);
        TEntity Get(string id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        // 2 - Adding objects
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        // 3 - Removing objects
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
