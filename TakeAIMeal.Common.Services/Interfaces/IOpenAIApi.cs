using Refit;
using TakeAIMeal.Common.Services.Models;

namespace TakeAIMeal.Common.Services.Interfaces
{
    public interface IOpenAIApi
    {
        [Post("/v1/completions")]
        Task<OpenAICompletionResponse> GetCompletions([Body] OpenAICompletionBody body);
    }
}
