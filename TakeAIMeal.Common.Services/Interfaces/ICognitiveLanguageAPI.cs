using Refit;
using TakeAIMeal.Common.Services.Models.AzureCognitive;

namespace TakeAIMeal.Common.Services.Interfaces
{
    /// <summary>
    /// Interface representing the Cognitive Language API.
    /// </summary>
    public interface ICognitiveLanguageAPI
    {
        /// <summary>
        /// Sends a request to the text recognition API to recognize entities in the provided text documents.
        /// </summary>
        /// <param name="body">The request body containing the documents to be processed.</param>
        /// <returns>The response containing recognized entities.</returns>
        [Post("/text/analytics/v3.1/entities/recognition/general")]
        Task<TextRecognitionResponse> GetTextRecognition([Body] TextRecognitionBody body);
    }
}
