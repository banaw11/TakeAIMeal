using TakeAIMeal.Common.Dictionaries;
using TakeAIMeal.Common.Services.Interfaces;
using TakeAIMeal.Common.Services.Models.OpenAI;

namespace TakeAIMeal.Common.Services.Logic
{
    public class TextGeneratorService : ITextGeneratorService
    {
        private readonly IOpenAIApi _openAIApi;

        public TextGeneratorService(IOpenAIApi openAIApi)
        {
            _openAIApi = openAIApi;
        }

        public async Task<ICollection<string>> GenerateText(string prompt, int completionCount = 1, float temperature = 0.2F, int maxTokens = 1024)
        {
            if (!string.IsNullOrEmpty(prompt))
            {
                var body = new OpenAICompletionBody
                {
                    Prompt = prompt,
                    Model = OpenAIModels.Davinci3,
                    N = completionCount,
                    Temperature = temperature,
                    Max_Tokens = maxTokens
                };

                try
                {
                    var result = await _openAIApi.GetCompletions(body);
                    return result?.Choices.Select(x => x.Text).ToList();
                }
                catch (Exception ex)
                {
                    //To-do error handling
                }
            }

            return null;
        }
    }
}
