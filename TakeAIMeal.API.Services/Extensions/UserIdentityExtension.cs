using System.Security.Claims;

namespace TakeAIMeal.API.Services.Extensions
{
    /// <summary>
    /// Provides extension methods for working with a <see cref="ClaimsPrincipal"/> object.
    /// </summary>
    public static class UserIdentityExtension
    {
        /// <summary>
        /// Gets the username of the user from the <see cref="ClaimsPrincipal"/> object.
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/> object.</param>
        /// <returns>The username of the user as a string, or null if the username could not be retrieved.</returns>
        public static string GetUserName(this ClaimsPrincipal principal)
        {
            return principal?.Identity?.Name;
        }

        /// <summary>
        /// Gets the email address of the user from the <see cref="ClaimsPrincipal"/> object.
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/> object.</param>
        /// <returns>The email address of the user as a string, or null if the email address could not be retrieved.</returns>
        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            return principal?.FindFirstValue(ClaimTypes.Email);
        }

        /// <summary>
        /// Gets the user ID of the user from the <see cref="ClaimsPrincipal"/> object.
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/> object.</param>
        /// <returns>The user ID of the user as an integer, or null if the user ID could not be retrieved.</returns>
        public static int? GetUserId(this ClaimsPrincipal principal)
        {
            int.TryParse(principal?.FindFirstValue(ClaimTypes.NameIdentifier), out int id);
            return id;
        }
    }
}
