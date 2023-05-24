using TakeAIMeal.Common.Dictionaries;

namespace TakeAIMeal.API.Services.Models
{
    /// <summary>
    /// Represents a model used for generating recipes.
    /// </summary>
    public class GenerateRecipeModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateRecipeModel"/> class.
        /// </summary>
        public GenerateRecipeModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateRecipeModel"/> class with the specified prompt, language, meal type, and recipe type.
        /// </summary>
        /// <param name="prompt">The prompt used for generating the recipe.</param>
        /// <param name="language">The language used for generating the recipe.</param>
        /// <param name="mealType">The type of meal for which the recipe is being generated.</param>
        /// <param name="recipeTypes">The type of recipe being generated.</param>
        public GenerateRecipeModel(string prompt, string language, MealTypes mealType, RecipeTypes recipeTypes)
        {
            Prompt = prompt;
            Language = language;
            MealType = mealType;
            RecipeType = recipeTypes;
        }

        /// <summary>
        /// Gets or sets the prompt used for generating the recipe.
        /// </summary>
        public string Prompt { get; set; }

        /// <summary>
        /// Gets or sets the language used for generating the recipe.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the type of meal for which the recipe is being generated.
        /// </summary>
        public MealTypes MealType { get; set; }

        /// <summary>
        /// Gets or sets the type of recipe being generated.
        /// </summary>
        public RecipeTypes RecipeType { get; set; }
    }
}
