namespace TakeAIMeal.API.Services.Interfaces
{
    /// <summary>
    /// Represents a user identity service.
    /// </summary>
    public interface IUserIdentityService
    {
        /// <summary>
        /// Gets the username associated with the user identity.
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gets the unique identifier of the user.
        /// </summary>
        int UserId { get; }

        /// <summary>
        /// Gets the email address associated with the user identity.
        /// </summary>
        string EmailAddress { get; }
    }
}
