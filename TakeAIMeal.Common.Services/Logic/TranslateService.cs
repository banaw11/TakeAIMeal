using TakeAIMeal.Common.Dictionaries;
using TakeAIMeal.Common.Services.Interfaces;
using TakeAIMeal.Common.Services.Models.AzureCognitive;

namespace TakeAIMeal.Common.Services.Logic
{
    public class TranslateService : ITranslateService
    {
        private readonly ICognitiveTranslateApi _translateApi;

        public TranslateService(ICognitiveTranslateApi translateApi)
        {
            _translateApi = translateApi;
        }

        public async Task<string> TranslateText(string origin, string to, string from = null)
        {
            if (!string.IsNullOrEmpty(origin) && !string.IsNullOrEmpty(to))
            {
                from ??= LanguageCodes.EN.ToString().ToLower();
                List<TextTranslation> body = new()
                {
                    new TextTranslation
                    {
                        Text = origin,
                    }
                };

                try
                {
                    var result = await _translateApi.GetTextTranslation(body, from, to);
                    return result?.Translations.FirstOrDefault()?.Text;
                }
                catch (Exception ex)
                {
                    //To-do error handling
                }
            }
            return null;
        }

        public async Task<ICollection<string>> TranslateTexts(ICollection<string> origin, string to, string from = null)
        {
            if (origin != null && origin.Count > 0 && !string.IsNullOrEmpty(to))
            {
                from ??= LanguageCodes.EN.ToString().ToLower();
                List<TextTranslation> body = origin.Select(x => new TextTranslation { Text = x }).ToList();

                try
                {
                    var result = await _translateApi.GetTextTranslation(body, from, to);
                    return result?.Translations.Select(x => x.Text).ToList();
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
