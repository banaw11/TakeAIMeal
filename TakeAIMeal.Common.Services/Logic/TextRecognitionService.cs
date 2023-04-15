using TakeAIMeal.Common.Services.Interfaces;
using TakeAIMeal.Common.Services.Models.AzureCognitive;

namespace TakeAIMeal.Common.Services.Logic
{
    public class TextRecognitionService : ITextRecognitionService
    {
        private readonly ICognitiveLanguageApi _cognitiveLanguageAPI;

        public TextRecognitionService(ICognitiveLanguageApi cognitiveLanguageAPI)
        {
            _cognitiveLanguageAPI = cognitiveLanguageAPI;
        }

        public async Task<ICollection<DocumentEntity>> GetTagsFromText(string text, string language = "en")
        {
            if (!string.IsNullOrEmpty(text))
            {
                TextRecognitionBody body = new TextRecognitionBody
                {
                    Documents = new List<RecognitionDocument>
                    {
                        new RecognitionDocument
                        {
                            Language = language,
                            Text = text
                        }
                    }
                };

                try
                {
                    var response = await _cognitiveLanguageAPI.GetTextRecognition(body);
                    if(response != null && response.Documents != null && response.Documents.Count > 0)
                    {
                        return response.Documents.FirstOrDefault().Entities;
                    }
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
