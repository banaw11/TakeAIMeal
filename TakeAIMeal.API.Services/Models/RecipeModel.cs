namespace TakeAIMeal.API.Services.Models
{
    /// <summary>
    /// Represents a recipe, including its title, recipe instructions, and image in base64 format.
    /// </summary>
    public class RecipeModel
    {
        /// <summary>
        /// The title of the recipe.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The instructions for preparing the recipe.
        /// </summary>
        public string Recipe { get; set; }

        /// <summary>
        /// The image of the recipe in base64 format.
        /// </summary>
        public string ImageBase64 { get; set; }
    }
}
