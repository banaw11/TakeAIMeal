namespace TakeAIMeal.API.Services.Interfaces
{
    /// <summary>
    /// Represents a service for retrieving tips.
    /// </summary>
    public interface ITipsService
    {
        /// <summary>
        /// Gets a collection of tips in the specified language.
        /// </summary>
        /// <param name="language">The language of the tips to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a collection of strings representing the tips.</returns>
        Task<ICollection<string>> GetTips(string language);
    }
}
