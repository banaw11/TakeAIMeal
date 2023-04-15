namespace TakeAIMeal.API.Models
{
    /// <summary>
    /// Represents a body object for generating a recipe.
    /// </summary>
    public class RecipeGenerateBody
    {
        /// <summary>
        /// Gets or sets the language to generate the recipe in.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the collection of ingredients to use in generating the recipe.
        /// </summary>
        public ICollection<string> Ingredients { get; set; }
    }
}
