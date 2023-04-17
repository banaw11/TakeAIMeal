namespace TakeAIMeal.Common.Options
{
    /// <summary>
    /// Represents the options for configuring an email client.
    /// </summary>
    public class EmailClientOption
    {
        /// <summary>
        /// Gets or sets the URL for the email client.
        /// </summary>
        /// <value>The URL for the email client.</value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the API key for the email client.
        /// </summary>
        /// <value>The API key for the email client.</value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the email address to use as the sender for emails sent by the client.
        /// </summary>
        /// <value>The email address to use as the sender for emails sent by the client.</value>
        public string EmailFrom { get; set; }
    }
}
