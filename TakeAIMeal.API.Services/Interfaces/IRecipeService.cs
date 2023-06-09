﻿using TakeAIMeal.API.Services.Models;
using TakeAIMeal.Common.Dictionaries;

namespace TakeAIMeal.API.Services.Interfaces
{
    /// <summary>
    /// Represents a service for generating recipes based on a given prompt.
    /// </summary>
    public interface IRecipeService
    {
        /// <summary>
        /// Generates a recipe based on the given prompt.
        /// </summary>
        /// <param name="model"><see cref="GenerateRecipeModel"/></param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated <see cref="RecipeModel"/>.</returns>
        Task<Tuple<RecipeModel, Guid>> GenerateRecipe(GenerateRecipeModel model);

        /// <summary>
        /// Gets a recipe ingredients string for the specified collection of product identifiers.
        /// </summary>
        /// <param name="productIds">The collection of product identifiers to get the recipe ingredients for.</param>
        /// <returns>A string representing the recipe ingredients for the specified collection of product identifiers.</returns>
        string GetRecipeIngridientsFromProducts(ICollection<int> productIds);

        /// <summary>
        /// Gets a recipe by identifier and language.
        /// </summary>
        /// <param name="identifier">The identifier of the recipe to retrieve.</param>
        /// <param name="language">The language in which the recipe is requested.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the retrieved recipe.</returns>
        Task<RecipeModel> GetRecipe(Guid identifier, string language);

        /// <summary>
        /// Retrieves a saved recipe based on the provided identifier and language.
        /// </summary>
        /// <param name="identifier">The unique identifier of the recipe.</param>
        /// <param name="language">The language in which the recipe should be retrieved.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains a tuple with the following:
        /// - RecipeModel: The retrieved recipe model.
        /// - int: The id number of related entity identifier.
        /// </returns>
        Task<Tuple<RecipeModel, int>> GetSavedRecipe(Guid identifier, string language);

        /// <summary>
        /// Adds a recipe using the specified <paramref name="model"/>.
        /// </summary>
        /// <param name="model">The reference model containing the recipe details.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the id of recipe</returns>
        Task<int?> AddRecipe(RecipeReferenceModel model);

        /// <summary>
        /// Removes the recipe with the specified <paramref name="recipeId"/>.
        /// </summary>
        /// <param name="recipeId">The ID of the recipe to be removed.</param>
        void RemoveRecipe(int recipeId);

        /// <summary>
        /// Retrieves a collection of recipe collections.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// The task result contains a collection of <see cref="RecipeCollectionModel"/> objects.
        /// </returns>
        Task<ICollection<RecipeCollectionModel>> GetRecipeCollection(string language);

        /// <summary>
        /// Retrieves a collection of exclussion product ids with the specified <paramref name="dietType"/>.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// The task result contains a collection of <see cref="int"/> objects.
        /// </returns>
        ICollection<int> GetUserProductExclussionsByDiet(DietTypes dietType);
    }
}
