using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using TakeAIMeal.Common.Options;

namespace TakeAIMeal.Common.Services.Handlers
{
    public class AuthorizationOpenAiApiHandler : DelegatingHandler
    {
        private readonly OpentAIApiOption _option;
        public AuthorizationOpenAiApiHandler(IOptions<OpentAIApiOption> options)
        {
            _option = options.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _option.ApiKey);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
