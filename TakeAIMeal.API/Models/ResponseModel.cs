namespace TakeAIMeal.API.Models
{
    /// <summary>
    /// Represents a standardized response model that includes a success status, an optional message, and data.
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Gets or sets an optional message associated with the response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data associated with the response.
        /// </summary>
        public dynamic Data { get; set; }
    }
}
