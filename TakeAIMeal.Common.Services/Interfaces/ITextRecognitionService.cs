using TakeAIMeal.Common.Services.Models.AzureCognitive;

namespace TakeAIMeal.Common.Services.Interfaces
{
    /// <summary>
    /// Represents a service for recognizing and extracting tags from text.
    /// </summary>
    public interface ITextRecognitionService
    {
        /// <summary>
        /// Extracts tags from the specified text using the specified language.
        /// </summary>
        /// <param name="text">The text from which to extract tags.</param>
        /// <param name="language">The language used to extract tags. Defaults to "en".</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="DocumentEntity"/> objects that represent the extracted tags.</returns>
        Task<ICollection<DocumentEntity>> GetTagsFromText(string text, string language = "en");
    }
}
