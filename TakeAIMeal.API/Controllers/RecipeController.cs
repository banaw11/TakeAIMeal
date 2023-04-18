using Microsoft.AspNetCore.Mvc;
using TakeAIMeal.API.Models.Recipe;
using TakeAIMeal.API.Services.Interfaces;
using TakeAIMeal.API.Services.Models;
using TakeAIMeal.Common.Resources;

namespace TakeAIMeal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

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
            if (ModelState.IsValid)
            {
                string prompt = string.Format(Prompts.RecipeFromIngredients, string.Join(", ", body.Ingredients));

                RecipeModel recipe = await _recipeService.GenerateRecipe(prompt, body.Language);

                if(recipe != null)
                {
                    return Ok(recipe);
                }

                return BadRequest("Something went wrong");
            }
            return BadRequest("Invalid body of request");
        }
    }
}
