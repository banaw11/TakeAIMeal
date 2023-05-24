using TakeAIMeal.Common.Dictionaries;

namespace TakeAIMeal.Data.Repositories.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        /// <summary>
        /// Adds a generated recipe log for the specified meal type and user ID.
        /// </summary>
        /// <param name="mealType">The type of meal for which the recipe log is being added.</param>
        /// <param name="userId">The ID of the user for whom the recipe log is being added. Set to null if the user ID is unknown.</param>
        /// <remarks>
        /// This method is used to add a log entry for a recipe that was generated for a specific meal type.
        /// The meal type specifies the category or type of the meal, such as breakfast, lunch, or dinner.
        /// The user ID parameter identifies the user for whom the recipe was generated. If the user ID is unknown,
        /// it can be set to null.
        /// </remarks>
        void AddGeneratedRecipeLog(MealTypes mealType, int? userId, RecipeTypes recipeType);
    }
}
