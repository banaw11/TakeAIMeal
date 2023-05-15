namespace TakeAIMeal.API.Models.Recipe
{
    /// <summary>
    /// Represents a recipe query object that specifies an identifier and language.
    /// </summary>
    public class RecipeQuery
    {
        /// <summary>
        /// Gets or sets the identifier of the recipe.
        /// </summary>
        public Guid Identifier { get; set; }

        /// <summary>
        /// Gets or sets the language of the recipe.
        /// </summary>
        public string Language { get; set; }
    }
}
