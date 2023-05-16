using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TakeAIMeal.Data.Repositories.Interfaces;

namespace TakeAIMeal.Data.Repositories.Infrastructure
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly ITakeAIMealDbContext _dbContext;

        public BaseRepository(ITakeAIMealDbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public void Add(T entity)
        {
            if(entity != null)
            {
                _dbSet.Add(entity);
            }
        }

        /// <inheritdoc/>
        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Any(expression);
        }

        /// <inheritdoc/>
        public void Delete(T entity)
        {
            if(entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        /// <inheritdoc/>
        public T Get(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).FirstOrDefault();
        }

        /// <inheritdoc/>
        public ICollection<T> GetAll()
        {
            return _dbSet.ToList();
        }

        /// <inheritdoc/>
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public void Update(T entity)
        {
            if(entity != null)
            {
                _dbSet.Update(entity);
            }
        }

        /// <inheritdoc/>
        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
