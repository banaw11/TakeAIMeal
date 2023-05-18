using System.ComponentModel.DataAnnotations;
using TakeAIMeal.Common.Dictionaries;

namespace TakeAIMeal.API.Models.Recipe
{
    /// <summary>
    /// Represents a class for generating random recipe body.
    /// </summary>
    public class RecipeGenerateRandomBody
    {
        /// <summary>
        /// Gets or sets the language of the recipe.
        /// </summary>
        [Required]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the meal type of the recipe.
        /// </summary>
        [Required]
        public MealTypes MealType { get; set; }
    }
}
