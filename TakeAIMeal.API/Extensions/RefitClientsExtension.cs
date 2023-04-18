using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using TakeAIMeal.Common.Options;
using TakeAIMeal.Common.Services.Handlers;
using TakeAIMeal.Common.Services.Interfaces;

namespace TakeAIMeal.API.Extensions
{
    public static class RefitClientsExtension
    {
        public static IServiceCollection AddRefitClients(this IServiceCollection services)
        {
            // Http message handler declaration area
            services.AddTransient<AuthorizationOpenAiApiHandler>();
            services.AddTransient<AuthorizationCognitiveLanguageApiHandler>();
            services.AddTransient<AuthorizationCognitiveTranslateApiHandler>();

            // Refit client declaration area
            services.AddRefitClient<IOpenAIApi>(new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                })
            }).ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = new Uri(serviceProvider.GetRequiredService<IOptions<OpentAIApiOption>>().Value.ApiUrl);
                httpClient.Timeout = TimeSpan.FromMinutes(10);
            }).AddHttpMessageHandler<AuthorizationOpenAiApiHandler>();

            services.AddRefitClient<ICognitiveLanguageApi>(new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                })
            }).ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = new Uri(serviceProvider.GetRequiredService<IOptions<CognitiveLanguageApiOption>>().Value.ApiUrl);
                httpClient.Timeout = TimeSpan.FromMinutes(10);
            }).AddHttpMessageHandler<AuthorizationCognitiveLanguageApiHandler>();

            services.AddRefitClient<ICognitiveTranslateApi>(new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                })
            }).ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = new Uri(serviceProvider.GetRequiredService<IOptions<CognitiveTranslateApiOption>>().Value.ApiUrl);
                httpClient.Timeout = TimeSpan.FromMinutes(10);
            }).AddHttpMessageHandler<AuthorizationCognitiveTranslateApiHandler>();

            return services;
        }
    }
}
