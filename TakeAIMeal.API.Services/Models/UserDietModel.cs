using TakeAIMeal.Common.Dictionaries;

namespace TakeAIMeal.API.Services.Models
{
    /// <summary>
    /// Represents a user's diet model.
    /// </summary>
    public class UserDietModel
    {
        /// <summary>
        /// Gets or sets the type of diet.
        /// </summary>
        public DietTypes DietType { get; set; }

        /// <summary>
        /// Gets or sets the collection of product exclusions.
        /// </summary>
        public ICollection<ProductExclusions> ProductExclusions { get; set; }
    }

    /// <summary>
    /// Represents product exclusions for a specific category.
    /// </summary>
    public class ProductExclusions
    {
        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the collection of product IDs.
        /// </summary>
        public ICollection<int> ProductIds { get; set; }
    }
}
