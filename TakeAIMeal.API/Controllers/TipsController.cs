﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeAIMeal.API.Models;
using TakeAIMeal.API.Services.Interfaces;

namespace TakeAIMeal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipsController : ControllerBase
    {
        private readonly ITipsService _tipsService;

        public TipsController(ITipsService tipsService)
        {
            _tipsService = tipsService;
        }

        /// <summary>
        /// Retrieves a collection of tips in the specified language.
        /// </summary>
        /// <param name="language">The language of the tips to retrieve.</param>
        /// <returns>Returns an <see cref="IActionResult"/> containing the collection of tips if the operation is successful. 
        /// If an error occurs, returns a <see cref="BadRequestObjectResult"/> with the error message.</returns>
        /// <response code="200">Returns a JSON array of strings containing the tips.</response>
        /// <response code="400">If the language parameter is missing or invalid, or an error occurs while retrieving the tips.</response>
        [HttpGet("{language}")]
        public async Task<IActionResult> GetTips(string language)
        {
            var responseModel = new ResponseModel();
            try
            {
                var tips = await _tipsService.GetTips(language);
                responseModel.Success = true;
                responseModel.Data = tips;
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Success = false;
                responseModel.Message = ex.Message;
                return BadRequest(responseModel);
            }
        }
    }
}
