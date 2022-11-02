using System.Linq.Expressions;

namespace AppFramework.Domain
{
    public interface IBaseRepository<TKey, TEntity> where TEntity : class
    {
        TEntity Get(TKey key);
        List<TEntity> Get();
        void Create(TEntity entity);
        bool Exists(Expression<Func<TEntity, bool>> expression);
        void SaveChanges();
    }
}
