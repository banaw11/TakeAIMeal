using Refit;
using TakeAIMeal.Common.Services.Models.OpenAI;

namespace TakeAIMeal.Common.Services.Interfaces
{
    /// <summary>
    /// Represents an interface for accessing the OpenAI API.
    /// </summary>
    public interface IOpenAIApi
    {
        /// <summary>
        /// Sends a request to the OpenAI API to get completions for a given prompt.
        /// </summary>
        /// <param name="body">The request body containing the prompt and other parameters.</param>
        /// <returns>A task representing the asynchronous operation. The result is an OpenAICompletionResponse object containing the completions returned by the API.</returns>
        [Post("/v1/completions")]
        Task<OpenAICompletionResponse> GetCompletions([Body] OpenAICompletionBody body);

        /// <summary>
        /// Sends a request to the OpenAI API to generate an image based on the specified parameters.
        /// </summary>
        /// <param name="body">The request body containing the parameters for generating the image.</param>
        /// <returns>A task representing the asynchronous operation. The result is an OpenAIImageResponse object containing the image data returned by the API.</returns>
        [Post("/v1/images/generations")]
        Task<OpenAIImageResponse> GetImageUrl([Body] OpenAIImageBody body);
    }
}
