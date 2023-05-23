using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TakeAIMeal.API.Models;
using TakeAIMeal.API.Services.Interfaces;
using TakeAIMeal.API.Services.Models;
using TakeAIMeal.Common.Dictionaries;

namespace TakeAIMeal.API.Controllers
{
    /// <summary>
    /// API controller for managing user diets.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DietController : ControllerBase
    {
        private readonly IUserDietService _userDietService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DietController"/> class.
        /// </summary>
        /// <param name="userDietService">The service for managing user diets.</param>
        public DietController(IUserDietService userDietService)
        {
            _userDietService = userDietService;
        }

        /// <summary>
        /// Updates a user's diet.
        /// </summary>
        /// <param name="model">The <see cref="UserDietModel"/> containing the updated diet information.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPatch("update")]
        public IActionResult PatchUserDiet([FromBody] UserDietModel model)
        {
            var response = new ResponseModel();
            if (ModelState.IsValid)
            {
                _userDietService.PatchDiet(model);
                return Ok(response);
            }
            response.Success = false;
            response.Message = "Invalid body of request";
            return BadRequest(response);
        }

        /// <summary>
        /// Retrieves the diet product exclusions based on the specified diet type.
        /// </summary>
        /// <param name="dietType">The type of diet for which to retrieve the product exclusions.</param>
        /// <returns>An IActionResult containing a response model with the retrieved diet product exclusions.</returns>
        [HttpGet("product-exclusions")]
        public IActionResult GetDietProductExclusions([FromQuery] DietTypes dietType)
        {
            var responseModel = new ResponseModel
            {
                Success = true,
                Data = _userDietService.GetUserProductExclusionsForDiet(dietType)
            };

            return Ok(responseModel);
        }
    }
}
