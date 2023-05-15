using TakeAIMeal.Data.Repositories.Infrastructure;
using TakeAIMeal.Data.Repositories.Interfaces;

namespace TakeAIMeal.Data.Repositories.Logic
{
    public class CategoryRepository : BaseRepository<ProductCategory>, ICategoryRepository
    {
        public CategoryRepository(ITakeAIMealDbContext dbContext) : base(dbContext)
        {
        }
    }
}
