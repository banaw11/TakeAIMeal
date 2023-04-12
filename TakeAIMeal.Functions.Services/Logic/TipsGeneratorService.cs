using Newtonsoft.Json;
using TakeAIMeal.Common.Dictionaries;
using TakeAIMeal.Common.Services.Interfaces;
using TakeAIMeal.Functions.Services.Interfaces;
using TakeAIMeal.Functions.Services.Models;

namespace TakeAIMeal.Functions.Services.Logic
{
    public class TipsGeneratorService : ITipsGeneratorService
    {
        private readonly ITextGeneratorService _textGeneratorService;
        private readonly ITranslateService _translateService;
        private readonly IBlobStorageService _blobStorageService;

        public TipsGeneratorService(ITextGeneratorService textGeneratorService, ITranslateService translateService, IBlobStorageService blobStorageService)
        {
            _textGeneratorService = textGeneratorService;
            _translateService = translateService;
            _blobStorageService = blobStorageService;
        }

        public async Task GenerateTips(int count, string subject)
        {
            if (!string.IsNullOrEmpty(subject))
            {
                var tips = await _textGeneratorService.GenerateText(subject, count, 1, 50);
                if(tips != null)
                {
                    List<TipModel> models = new List<TipModel>();

                    foreach (var tip in tips)
                    {
                        var translation = await _translateService.TranslateText(tip, LanguageCodes.PL.ToString().ToLower());
                        models.Add(new TipModel { Language = LanguageCodes.EN.ToString().ToLower(), Text = tip });
                        models.Add(new TipModel { Language = LanguageCodes.PL.ToString().ToLower(), Text = translation });
                    }

                    var serializedModels = JsonConvert.SerializeObject(models);
                    await _blobStorageService.UploadStringContent(serializedModels, BlobStorageContainerNames.TipsContainer, BlobStorageBlobNames.TipFile);
                }
            }
        }
    }
}
