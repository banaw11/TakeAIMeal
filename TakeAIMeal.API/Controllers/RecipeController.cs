using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TakeAIMeal.API.Models;
using TakeAIMeal.API.Models.Recipe;
using TakeAIMeal.API.Services.Interfaces;
using TakeAIMeal.API.Services.Models;
using TakeAIMeal.Common.Extensions;
using TakeAIMeal.Common.Resources;

namespace TakeAIMeal.API.Controllers
{
    /// <summary>
    /// Provides APIs for accessing recipe infrastructure.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeController"/> class.
        /// </summary>
        /// <param name="recipeService">The service for accessing recipe service.</param>
        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        /// <summary>
        /// Generates a recipe based on the provided ingredients and language.
        /// </summary>
        /// <param name="body">The request body object containing the ingredients and language.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an IActionResult object that represents the response of the operation.</returns>
        /// <response code="200">Returns the generated recipe model.</response>
        /// <response code="400">Returned if the request body is invalid or if something goes wrong with the generation process.</response>
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateRecipe([FromBody] RecipeGenerateBody body)
        {
            var response = new ResponseModel
            {
                Success = false
            };
            if (ModelState.IsValid)
            {
                string ingredients = _recipeService.GetRecipeIngridientsFromProducts(body.Products);
                string mealType = body.MealType.GetAttribute<DisplayAttribute>().Name.ToLower();
                string prompt = string.Format(Prompts.RecipeFromIngredients, ingredients, mealType);

                var result = await _recipeService.GenerateRecipe(prompt, body.Language);

                if(result != null)
                {
                    response.Success = true;
                    response.Data = new { model = result.Item1 , identifier = result.Item2.ToString() };
                    return Ok(response);
                }

                response.Message = "Something went wrong";
                return BadRequest(response);
            }
            response.Message = "Invalid body of request";
            return BadRequest(response);
        }

        /// <summary>
        /// Retrieves a recipe based on the specified query parameters.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains an <see cref="IActionResult"/>
        /// indicating the success or failure of the operation, and the retrieved recipe if successful.
        /// </returns>
        [HttpGet("get-recipe")]
        [ResponseCache(Duration = 3600)]
        public async Task<IActionResult> GetRecipe([FromQuery] RecipeQuery query)
        {
            var response = new ResponseModel
            {
                Success = false
            };
            var result = await _recipeService.GetRecipe(query.Identifier, query.Language);
            if(result != null)
            {
                response.Success = true;
                response.Data = result;
                return Ok(response);
            }

            response.Message = "Query is missing required values";
            return BadRequest(response);
        }

        /// <summary>
        /// Saves a recipe using the specified <paramref name="model"/>.
        /// </summary>
        /// <param name="model">The recipe reference model containing the recipe details.</param>
        /// <returns>An <see cref="IActionResult"/> representing the response of the save operation.</returns>
        /// <remarks>
        /// This endpoint requires the user to be authenticated.
        /// </remarks>
        [Authorize]
        [HttpPost("save-recipe")]
        public async Task<IActionResult> SaveRecipe([FromBody] RecipeReferenceModel model)
        {
            var response = new ResponseModel
            {
                Success = false
            };
            if (ModelState.IsValid)
            {
                var result = await _recipeService.AddRecipe(model);
                if (result.HasValue)
                {
                    response.Success = true;
                    response.Data = result;
                }
            }

            response.Message = "Body is missing required values";
            return BadRequest(response);
        }

        /// <summary>
        /// Deletes a recipe with the specified <paramref name="recipeId"/>.
        /// </summary>
        /// <param name="recipeId">The ID of the recipe to be deleted.</param>
        /// <returns>An <see cref="IActionResult"/> representing the response of the delete operation.</returns>
        /// <remarks>
        /// This endpoint requires the user to be authenticated.
        /// </remarks>
        [Authorize]
        [HttpDelete("delete-recipe/{recipeId}")]
        public IActionResult DeleteRecipe(int recipeId)
        {
            var response = new ResponseModel
            {
                Success = true
            };
            _recipeService.RemoveRecipe(recipeId);

            return Ok(response);
        }

        /// <summary>
        /// Retrieves a list of recipe collections.
        /// </summary>
        /// <param name="language">The language to use for retrieving the recipe collections.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> representing the HTTP response.
        /// The response contains a <see cref="ResponseModel"/> object with the retrieved recipe collections in the <see cref="ResponseModel.Data"/> property.
        /// </returns>
        /// <remarks>
        /// This endpoint requires the user to be authenticated.
        /// </remarks>
        [Authorize]
        [HttpGet("get-list")]
        public async Task<IActionResult> GetRecipeList([FromBody] string language)
        {
            var response = new ResponseModel
            {
                Success = true
            };

            var collection = await _recipeService.GetRecipeCollection(language);
            response.Data = collection;

            return Ok(response);
        }
    }
}
