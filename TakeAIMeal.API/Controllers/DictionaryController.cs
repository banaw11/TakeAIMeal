using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TakeAIMeal.API.Models;
using TakeAIMeal.API.Services.Interfaces;

namespace TakeAIMeal.API.Controllers
{
    /// <summary>
    /// Provides APIs for accessing dictionary items related to meals, categories and products.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly IDictionarySerivce _dictionarySerivce;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryController"/> class.
        /// </summary>
        /// <param name="dictionarySerivce">The service for accessing dictionary items.</param>
        public DictionaryController(IDictionarySerivce dictionarySerivce)
        {
            _dictionarySerivce = dictionarySerivce;
        }

        /// <summary>
        /// Gets a list of meal types.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing a response model with a list of meal types.</returns>
        [HttpGet("meal-types")]
        public IActionResult GetMealTypes()
        {
            var responseModel = new ResponseModel
            {
                Success = true,
                Data = _dictionarySerivce.GetMeals()
            };

            return Ok(responseModel);
        }

        /// <summary>
        /// Gets a list of category types.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing a response model with a list of category types.</returns>
        [HttpGet("category-types")]
        public IActionResult GetCategoryTypes()
        {
            var responseModel = new ResponseModel
            {
                Success = true,
                Data = _dictionarySerivce.GetCategories()
            };

            return Ok(responseModel);
        }

        /// <summary>
        /// Gets a list of product types for the specified <paramref name="categoryId"/>.
        /// </summary>
        /// <param name="categoryId">The identifier of the category to get the product types for.</param>
        /// <returns>An <see cref="IActionResult"/> containing a response model with a list of product types for the specified <paramref name="categoryId"/>.</returns>
        [HttpGet("product-types/{categoryId}")]
        public IActionResult GetProductTypes(int categoryId)
        {
            var responseModel = new ResponseModel
            {
                Success = true,
                Data = _dictionarySerivce.GetProducts(categoryId)
            };

            return Ok(responseModel);
        }

        /// <summary>
        /// Retrieves all diet types.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing the response with a collection of diet types.</returns>
        [HttpGet("diet-all-types")]
        public IActionResult GetDietAllTypes()
        {
            var responseModel = new ResponseModel
            {
                Success = true,
                Data = _dictionarySerivce.GetAllDiets()
            };

            return Ok(responseModel);
        }

        /// <summary>
        /// Retrieves used diet types.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing the response with a collection of used diet types.</returns>
        /// <remarks>
        /// This endpoint requires the user to be authenticated.
        /// </remarks>
        [Authorize]
        [HttpGet("diet-used-types")]
        public IActionResult GetDietUsedTypes()
        {
            var responseModel = new ResponseModel
            {
                Success = true,
                Data = _dictionarySerivce.GetUsedDiets()
            };

            return Ok(responseModel);
        }
    }
}
