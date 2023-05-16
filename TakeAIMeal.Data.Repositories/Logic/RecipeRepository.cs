using TakeAIMeal.Data.Repositories.Infrastructure;
using TakeAIMeal.Data.Repositories.Interfaces;

namespace TakeAIMeal.Data.Repositories.Logic
{
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ITakeAIMealDbContext dbContext) : base(dbContext)
        {
        }
    }
}
