using TakeAIMeal.API.Services.Models;
using TakeAIMeal.Common.Dictionaries;

namespace TakeAIMeal.API.Services.Interfaces
{
    /// <summary>
    /// Represents a service for managing user diets.
    /// </summary>
    public interface IUserDietService
    {
        /// <summary>
        /// Updates the user's diet with the provided model.
        /// </summary>
        /// <param name="model">The <see cref="UserDietModel"/> containing the updated diet information.</param>
        void PatchDiet(UserDietModel model);

        /// <summary>
        /// Retrieves the product exclusions for a specific diet type.
        /// </summary>
        /// <param name="dietType">The type of diet for which to retrieve the product exclusions.</param>
        /// <returns>A collection of product exclusions associated with the specified diet type.</returns>
        ICollection<ProductExclusions> GetUserProductExclusionsForDiet(DietTypes dietType);
        
    }
}
