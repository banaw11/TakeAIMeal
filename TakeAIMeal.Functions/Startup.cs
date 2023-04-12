using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using System;
using TakeAIMeal.Common.Options;
using TakeAIMeal.Common.Services.Handlers;
using TakeAIMeal.Common.Services.Interfaces;
using TakeAIMeal.Common.Services.Logic;
using TakeAIMeal.Functions;
using TakeAIMeal.Functions.Services.Interfaces;
using TakeAIMeal.Functions.Services.Logic;

[assembly: FunctionsStartup(typeof(Startup))]
namespace TakeAIMeal.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddLogging();

            // Bind configuration section into option
            builder.Services.AddOptions<OpentAIApiOption>().Bind(configuration.GetSection("OpenAI"));
            builder.Services.AddOptions<CognitiveTranslateApiOption>().Bind(configuration.GetSection("CognitiveTranslate"));

            // Http message handler declaration area
            builder.Services.AddTransient<AuthorizationCognitiveTranslateApiHandler>();
            builder.Services.AddTransient<AuthorizationOpenAIApiHandler>();

            // Refit client declaration area
            builder.Services.AddRefitClient<IOpenAIApi>(new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                })
            }).ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = new Uri(serviceProvider.GetRequiredService<IOptions<OpentAIApiOption>>().Value.ApiUrl);
                httpClient.Timeout = TimeSpan.FromMinutes(10);
            }).AddHttpMessageHandler<AuthorizationOpenAIApiHandler>();

            builder.Services.AddRefitClient<ICognitiveTranslateApi>(new RefitSettings
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

            // DI services
            builder.Services.AddScoped<ITextGeneratorService, TextGeneratorService>();
            builder.Services.AddScoped<ITranslateService, TranslateService>();
            builder.Services.AddScoped<IBlobStorageService, BlobStorageService>(provider => new BlobStorageService(configuration.GetConnectionString("StorageAccount")));
            builder.Services.AddScoped<ITipsGeneratorService, TipsGeneratorService>();
        }
    }
}
