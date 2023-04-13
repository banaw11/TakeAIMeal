using Newtonsoft.Json;
using TakeAIMeal.API.Services.Interfaces;
using TakeAIMeal.Common.Dictionaries;
using TakeAIMeal.Common.Services.Interfaces;
using TakeAIMeal.Functions.Services.Models;

namespace TakeAIMeal.API.Services.Logic
{
    public class TipsService : ITipsService
    {
        private readonly IBlobStorageService _blobStorageService;

        public TipsService(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        public async Task<ICollection<string>> GetTips(string language)
        {
            List<string> tips = new List<string>();

            var tipsJson = await _blobStorageService.DownloadStringContent(BlobStorageContainerNames.TipsContainer, BlobStorageBlobNames.TipFile);
            if (!string.IsNullOrEmpty(tipsJson))
            {
                List<TipModel> tipModels = JsonConvert.DeserializeObject<List<TipModel>>(tipsJson);

                if(tipModels != null && tipModels.Count > 0)
                {
                    tips = tipModels.Where(x => x.Language == language)
                        .Select(x => x.Text.Replace("\n", ""))
                        .ToList();

                }
            }

            return tips;
        }
    }
}
