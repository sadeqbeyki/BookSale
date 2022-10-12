using AppFramework.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppFramework.Infrastructure
{
    public class BaseRepository<TKey, TEntity> : IBaseRepository<TKey, TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public void Create(TEntity entity)
        {
            _context.Add(entity);
        }

        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Any(expression);
        }

        public TEntity? Get(TKey id)
        {
            return _context.Find<TEntity>(id);
        }

        public List<TEntity> Get()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
