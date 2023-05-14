using Microsoft.AspNetCore.Mvc;
using TakeAIMeal.API.Models;

namespace TakeAIMeal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        public DictionaryController()
        {
        }

        [HttpGet("meal-types")]
        public IActionResult GetMealTypes()
        {
            var responseModel = new ResponseModel();
            responseModel.Success = true;
            responseModel.Data = new List<object>
            {
                new {Name = "Dinner", Value = 0},
                new {Name = "Brekfast", Value = 1},
                new {Name = "Supper", Value = 2}
            };

            return Ok(responseModel);
        }

        [HttpGet("category-types")]
        public IActionResult GetCategoryTypes()
        {
            var responseModel = new ResponseModel();
            responseModel.Success = true;
            responseModel.Data = new List<object>
            {
                new {Name = "Vegetables", Value = 0},
                new {Name = "Fruits", Value = 1},
                new {Name = "Meat", Value = 2}
            };

            return Ok(responseModel);
        }

        [HttpGet("product-types/{categoryId}")]
        public IActionResult GetCategoryTypes(int categoryId)
        {
            var responseModel = new ResponseModel();
            responseModel.Success = true;
            responseModel.Data = new List<object>
            {
                new {Name = "Carrot", Value = 0},
                new {Name = "Tomato", Value = 1},
                new {Name = "Onion", Value = 2}
            };

            return Ok(responseModel);
        }
    }
}
