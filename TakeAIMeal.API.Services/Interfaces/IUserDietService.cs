using TakeAIMeal.API.Services.Models;

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
    }
}
