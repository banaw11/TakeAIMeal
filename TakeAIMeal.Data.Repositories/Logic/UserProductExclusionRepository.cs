using TakeAIMeal.Data.Repositories.Infrastructure;
using TakeAIMeal.Data.Repositories.Interfaces;

namespace TakeAIMeal.Data.Repositories.Logic
{
    public class UserProductExclusionRepository : BaseRepository<UserProductsExclusion>, IUserProductExclusionRepository
    {
        public UserProductExclusionRepository(ITakeAIMealDbContext dbContext) : base(dbContext)
        {
        }
    }
}
