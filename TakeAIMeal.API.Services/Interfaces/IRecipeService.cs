using TakeAIMeal.API.Services.Models;

namespace TakeAIMeal.API.Services.Interfaces
{
    /// <summary>
    /// Represents a service for generating recipes based on a given prompt.
    /// </summary>
    public interface IRecipeService
    {
        /// <summary>
        /// Generates a recipe based on the given prompt.
        /// </summary>
        /// <param name="prompt">The prompt used to generate the recipe.</param>
        /// <param name="language">The language used to generate the recipe.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated <see cref="RecipeModel"/>.</returns>
        Task<RecipeModel> GenerateRecipe(string prompt, string language);
    }
}
