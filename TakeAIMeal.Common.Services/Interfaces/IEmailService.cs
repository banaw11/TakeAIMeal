namespace TakeAIMeal.Common.Services.Interfaces
{
    /// <summary>
    /// Defines an interface for sending email messages.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email message to the specified recipient with the given subject and body.
        /// </summary>
        /// <param name="to">The email address of the recipient.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The body of the email.</param>
        /// <returns>A task that represents the asynchronous operation of sending the email.</returns>
        Task SendEmail(string to, string subject, string body);
    }
}
