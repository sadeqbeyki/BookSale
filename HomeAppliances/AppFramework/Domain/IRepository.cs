using System.Linq.Expressions;

namespace AppFramework.Domain
{
    public interface IRepository<TKey, T> where T : class
    {
        T Get(TKey id);
        List<T> Get();
        void Create(T entity);
        bool Exists(Expression<Func<T, bool>> expression);
        void SaveChanges();
    }
}
