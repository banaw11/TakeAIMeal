using System.ComponentModel.DataAnnotations;

namespace TakeAIMeal.API.Models.Account
{
    /// <summary>
    /// Represents the sign-in request body.
    /// </summary>
    public class SignInBody
    {
        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        /// <value>The email address of the user.</value>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        /// <value>The password of the user.</value>
        [Required]
        public string Password { get; set; }
    }
}
