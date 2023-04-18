using System.ComponentModel.DataAnnotations;

namespace TakeAIMeal.API.Models.Account
{
    /// <summary>
    /// Represents the data required to confirm a user's email address.
    /// </summary>
    public class EmailConfirmBody
    {
        /// <summary>
        /// Gets or sets the email address of the user to confirm.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the confirmation code for the user's email address.
        /// </summary>
        [Required]
        public string Code { get; set; }
    }
}
