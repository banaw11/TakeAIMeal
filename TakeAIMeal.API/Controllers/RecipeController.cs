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
    }
}
