namespace TakeAIMeal.API.Services.Models
{
    /// <summary>
    /// Represents a recipe, including its title, recipe instructions, and image in base64 format.
    /// </summary>
    public class RecipeModel : ICloneable
    {
        public RecipeModel()
        {
        }

        public RecipeModel(RecipeModel model)
        {
            Title = model.Title;
            Recipe = model.Recipe;
            ImageBase64 = model.ImageBase64;
        }

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

        public object Clone()
        {
            return new RecipeModel(this);
        }
    }
}
