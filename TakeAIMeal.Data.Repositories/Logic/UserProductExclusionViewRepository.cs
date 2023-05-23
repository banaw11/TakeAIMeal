using System.Linq.Expressions;
using TakeAIMeal.Data.Repositories.Interfaces;

namespace TakeAIMeal.Data.Repositories.Logic
{
    public class UserProductExclusionViewRepository : IUserProductExclusionViewRepository
    {
        private readonly ITakeAIMealDbContext _dbContext;

        public UserProductExclusionViewRepository(ITakeAIMealDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public UserProductExclusionView Get(Expression<Func<UserProductExclusionView, bool>> expression)
        {
            return _dbContext.UserProductExclusionViews.FirstOrDefault(expression);
        }

        /// <inheritdoc/>
        public IQueryable<UserProductExclusionView> Where(Expression<Func<UserProductExclusionView, bool>> expression)
        {
            return _dbContext.UserProductExclusionViews.Where(expression);
        }
    }
}
