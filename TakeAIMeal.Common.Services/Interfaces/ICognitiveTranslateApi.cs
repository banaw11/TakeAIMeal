using Refit;
using TakeAIMeal.Common.Services.Models.AzureCognitive;

namespace TakeAIMeal.Common.Services.Interfaces
{
    /// <summary>
    /// Represents an interface for accessing the Cognitive Services text translation API.
    /// </summary>
    public interface ICognitiveTranslateApi
    {
        /// <summary>
        /// Sends a POST request to the text translation API to translate a collection of texts from the specified origin language to the specified target language.
        /// </summary>
        /// <param name="body">The collection of TextTranslation objects to be translated.</param>
        /// <param name="from">The origin language code.</param>
        /// <param name="to">The target language code.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a TextTranslationResponse object that represents the response from the API.</returns>
        [Post("/translate?api-version=3.0&from={from}&to={to}")]
        Task<TextTranslationResponse> GetTextTranslation([Body] ICollection<TextTranslation> body, [AliasAs("from")] string from, [AliasAs("to")] string to);
    }
}
