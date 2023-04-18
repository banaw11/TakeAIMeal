namespace TakeAIMeal.Functions.Services.Interfaces
{
    /// <summary>
    /// Represents a service for generating tips.
    /// </summary>
    public interface ITipsGeneratorService
    {
        /// <summary>
        /// Generates the specified number of tips for the given subject.
        /// </summary>
        /// <param name="count">The number of tips to generate.</param>
        /// <param name="subject">The subject for which to generate tips.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task GenerateTips(int count, string subject);
    }
}
