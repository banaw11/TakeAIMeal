using TakeAIMeal.Common.Services.Interfaces;
using TakeAIMeal.Common.Services.Models.OpenAI;

namespace TakeAIMeal.Common.Services.Logic
{
    public class ImageService : IImageService
    {
        private readonly IOpenAIApi _openAIApi;

        public ImageService(IOpenAIApi openAIApi)
        {
            _openAIApi = openAIApi;
        }

        public async Task<string> GenerateImageToBase64(string prompt, string size = "256x256")
        {
            if (!string.IsNullOrEmpty(prompt))
            {
                OpenAIImageBody body = new OpenAIImageBody
                {
                    N = 1,
                    Prompt = prompt,
                    Response_type = "b64_json",
                    Size = size
                };

                var response = await _openAIApi.GetImage(body);

                if(response != null && response.Data != null && response.Data.Count > 0)
                {
                    return response.Data.FirstOrDefault().B64_json;
                }
            }

            return null;
        }
    }
}
