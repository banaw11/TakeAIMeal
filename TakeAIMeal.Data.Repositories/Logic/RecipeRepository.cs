using TakeAIMeal.Common.Dictionaries;
using TakeAIMeal.Data.Repositories.Infrastructure;
using TakeAIMeal.Data.Repositories.Interfaces;

namespace TakeAIMeal.Data.Repositories.Logic
{
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        private readonly ITakeAIMealDbContext _dbContext;

        public RecipeRepository(ITakeAIMealDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddGeneratedRecipeLog(MealTypes mealType, int? userId)
        {
            _dbContext.AddGeneretedRecipe((int)mealType, userId);
        }
    }
}
