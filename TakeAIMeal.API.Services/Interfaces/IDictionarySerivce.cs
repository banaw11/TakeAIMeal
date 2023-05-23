using TakeAIMeal.API.Services.Models;

namespace TakeAIMeal.API.Services.Interfaces
{
    /// <summary>
    /// Represents a service for accessing and managing dictionaries of items.
    /// </summary>
    public interface IDictionarySerivce
    {
        /// <summary>
        /// Gets all the products in the specified <paramref name="categoryId"/>.
        /// </summary>
        /// <param name="categoryId">The identifier of the category to get the products for.</param>
        /// <returns>A collection of <see cref="DictionaryItem"/> representing the products in the specified <paramref name="categoryId"/>.</returns>
        ICollection<DictionaryItem> GetProducts(int categoryId);

        /// <summary>
        /// Gets all the categories.
        /// </summary>
        /// <returns>A collection of <see cref="DictionaryItem"/> representing all the categories.</returns>
        ICollection<DictionaryItem> GetCategories();

        /// <summary>
        /// Gets all the meals.
        /// </summary>
        /// <returns>A collection of <see cref="DictionaryItem"/> representing all the meals.</returns>
        ICollection<DictionaryItem> GetMeals();

        /// <summary>
        /// Retrieves all diets as a collection of dictionary items.
        /// </summary>
        /// <returns>A collection of <see cref="DictionaryItem"/> representing all diets.</returns>
        ICollection<DictionaryItem> GetAllDiets();

        /// <summary>
        /// Retrieves the used diets as a collection of dictionary items.
        /// </summary>
        /// <returns>A collection of <see cref="DictionaryItem"/> representing the used diets.</returns>
        ICollection<DictionaryItem> GetUsedDiets();
    }
}
