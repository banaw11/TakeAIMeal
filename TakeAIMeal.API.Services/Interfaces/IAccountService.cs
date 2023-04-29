using TakeAIMeal.API.Services.Models;

namespace TakeAIMeal.API.Services.Interfaces
{
    /// <summary>
    /// Provides functionality to manage user accounts.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Signs in a user with the specified email and password.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<UserModel> SignInAsync(string email, string password);

        /// <summary>
        /// Signs out the current user.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SignOutAsync();

        /// <summary>
        /// Registers a new user account with the specified email, password, and username.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="username">The username of the user.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<bool> RegisterAccountAsync(string email, string password, string username);

        /// <summary>
        /// Confirms the email address of a user with the specified email and code.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="code">The code used to confirm the email address.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<bool> ConfirmEmailAsync(string email, string code);

        /// <summary>
        /// Returns the currently signed-in user.
        /// </summary>
        /// <returns>A <see cref="UserModel"/> object representing the signed-in user, or null if no user is signed in.</returns>
        UserModel GetSignedUser();
    }
}
