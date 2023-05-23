using TakeAIMeal.Data.Repositories.Infrastructure;
using TakeAIMeal.Data.Repositories.Interfaces;

namespace TakeAIMeal.Data.Repositories.Logic
{
    public class UserDietRepository : BaseRepository<UserDiet>, IUserDietRepository
    {
        public UserDietRepository(ITakeAIMealDbContext dbContext) : base(dbContext)
        {
        }
    }
}
