using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TakeAIMeal.API.Models;
using TakeAIMeal.API.Models.Account;
using TakeAIMeal.API.Services.Interfaces;

namespace TakeAIMeal.API.Controllers
{
    /// <summary>
    /// Represents a controller for handling user account-related API requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="accountService">The <see cref="IAccountService"/> implementation to use for account-related operations.</param>
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Attempts to sign in a user with the specified email and password.
        /// </summary>
        /// <param name="body">The json object containing the email and password of the user to sign in.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the sign-in attempt.</returns>
        /// <response code="200">The user was signed in successfully.</response>
        /// <response code="400">The form data was invalid or the user could not be signed in.</response>
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInBody body)
        {
            var responseModel = new ResponseModel();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _accountService.SignInAsync(body.Email, body.Password);
                    responseModel.Success = true;
                    responseModel.Data = user;
                    return Ok(responseModel);
                }
                catch(Exception ex)
                {
                    responseModel.Message = ex.Message;
                    responseModel.Success = false;
                    return BadRequest(responseModel);
                }
            }

            responseModel.Success = false;
            responseModel.Message = "Invalid form data";
            return BadRequest(responseModel);
        }

        /// <summary>
        /// Signs out the current user.
        /// </summary>
        /// <returns>
        /// Returns an IActionResult representing the result of the operation:
        /// </returns>
        /// <response code="204">The user was signed out successfully.</response>
        /// <remarks>
        /// This endpoint requires the user to be authenticated.
        /// </remarks>
        [Authorize]
        [HttpPost("sign-out")]
        public async Task<IActionResult> SignOutUser()
        {
            await _accountService.SignOutAsync();
            return NoContent();
        }

        /// <summary>
        /// Checks the session of the currently signed-in user.
        /// </summary>
        /// <returns>An IActionResult containing a ResponseModel indicating whether the user is signed in and the user's information if applicable.</returns>
        /// <response code="200">Returns current signed in user's information if applicable.</response>
        [HttpGet("check-session")]
        public IActionResult CheckSession()
        {
            var user = _accountService.GetSignedUser();
            var responseModel = new ResponseModel
            {
                Success = user != null,
                Data = user
            };
            return Ok(responseModel);
        }

        /// <summary>
        /// Registers a new account with the provided email, password, and username.
        /// </summary>
        /// <param name="body">A SignUpBody object containing the email, password, and username of the user.</param>
        /// <returns>
        /// Returns an IActionResult representing the result of the operation:
        /// </returns>
        /// <response code="200">The account was registered successfully.</response>
        /// <response code="400">The form data was invalid or the account could not be registered.</response>
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpBody body)
        {
            var responseModel = new ResponseModel();
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _accountService.RegisterAccountAsync(body.Email, body.Password, body.Username);
                    if (result)
                    {
                        return Ok();
                    }

                    responseModel.Success = false;
                    responseModel.Message = "Something went wrong, try again later";
                    return BadRequest(responseModel);
                    
                }
                catch (Exception ex)
                {
                    responseModel.Success = false;
                    responseModel.Message = ex.Message;
                    return BadRequest(responseModel);
                }
            }

            responseModel.Success = false;
            responseModel.Message = "Invalid form data";
            return BadRequest(responseModel);
        }

        /// <summary>
        /// Confirms the user's email address with the provided email and code.
        /// </summary>
        /// <param name="body">An EmailConfirmBody object containing the email and code of the user's email confirmation token.</param>
        /// <returns>
        /// Returns an IActionResult representing the result of the operation:
        /// </returns>
        /// <response code="200">The email was confirmed successfully.</response>
        /// <response code="400">The form data was invalid or the email could not be confirmed.</response>
        [HttpPost("email-confirm")]
        public async Task<IActionResult> EmailConfirm([FromBody] EmailConfirmBody body)
        {
            var responseModel = new ResponseModel();
            if (ModelState.IsValid)
            {
                try
                {
                    if(await _accountService.ConfirmEmailAsync(body.Email, body.Code))
                    {
                        return Ok(responseModel);
                    }
                    
                }
                catch (Exception ex)
                {
                    responseModel.Success = false;
                    responseModel.Message = ex.Message;
                    return BadRequest(responseModel);
                }
            }

            responseModel.Success = false;
            responseModel.Message = "Invalid form data";
            return BadRequest(responseModel);
        }
    }
}
