using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using TakeAIMeal.Common.Options;

namespace TakeAIMeal.Common.Services.Handlers
{
    public class AuthorizationCognitiveLanguageApiHandler : DelegatingHandler
    {
        private readonly CognitiveLanguageApiOption _option;
        public AuthorizationCognitiveLanguageApiHandler(IOptions<CognitiveLanguageApiOption> options)
        {
            _option = options.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Ocp-Apim-Subscription-Key", _option.ApiKey);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
