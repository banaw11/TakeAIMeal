using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using TakeAIMeal.Common.Resources;
using TakeAIMeal.Functions.Services.Interfaces;

namespace TakeAIMeal.Functions
{
    public class TipsGeneratorFunction
    {
        private readonly ITipsGeneratorService _tipsGeneratorService;

        public TipsGeneratorFunction(ITipsGeneratorService tipsGeneratorService)
        {
            _tipsGeneratorService = tipsGeneratorService;
        }

        [FunctionName(nameof(TipsGeneratorFunction))]
        public async Task Run([TimerTrigger("0 0 8-18 * * *", RunOnStartup = true)]TimerInfo myTimer, ILogger log)
        {
            try
            {
                await _tipsGeneratorService.GenerateTips(4, Prompts.TipHealthyEating);
            }
            catch (Exception ex)
            {
                log.LogError("Error occured during tips generation {Message}", ex);
            }
        }
    }
}
