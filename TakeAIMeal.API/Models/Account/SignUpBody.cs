using System.ComponentModel.DataAnnotations;

namespace TakeAIMeal.API.Models.Account
{
    /// <summary>
    /// Represents the data required to sign up a new user.
    /// </summary>
    public class SignUpBody
    {
        /// <summary>
        /// Gets or sets the email address of the new user.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password for the new user.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the username for the new user.
        /// </summary>
        [Required]
        public string Username { get; set; }
    }
}
