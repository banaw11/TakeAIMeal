namespace TakeAIMeal.API.Services.Models
{
    /// <summary>
    /// Represents a model for a recipe collection.
    /// </summary>
    public class RecipeCollectionModel
    {
        /// <summary>
        /// Gets or sets the base64-encoded image associated with the recipe collection.
        /// </summary>
        public string ImageBase64 { get; set; }

        /// <summary>
        /// Gets or sets the title of the recipe collection.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the recipe collection.
        /// </summary>
        public Guid RecipeIdentifier { get; set; }
    }
}
