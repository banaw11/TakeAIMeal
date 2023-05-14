using System.ComponentModel.DataAnnotations;
using TakeAIMeal.Common.Dictionaries;

namespace TakeAIMeal.API.Models.Recipe
{
    /// <summary>
    /// Represents a body object for generating a recipe.
    /// </summary>
    public class RecipeGenerateBody
    {
        /// <summary>
        /// Gets or sets the language to generate the recipe in.
        /// </summary>
        [Required]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the enum of meal type to use in generating the recipe.
        /// </summary>
        [Required]
        public MealTypes MealType { get; set; }

        /// <summary>
        /// Gets or sets the collection of product id to use in generating the recipe.
        /// </summary>
        [Required]
        public ICollection<int> Products { get; set; }
    }
}
