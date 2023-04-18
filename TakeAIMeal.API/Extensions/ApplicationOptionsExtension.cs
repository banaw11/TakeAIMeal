using TakeAIMeal.Common.Options;

namespace TakeAIMeal.API.Extensions
{
    public static class ApplicationOptionsExtension
    {
        public static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<OpentAIApiOption>().Bind(configuration.GetSection("OpenAI"));
            services.AddOptions<CognitiveLanguageApiOption>().Bind(configuration.GetSection("CognitiveLanguage"));
            services.AddOptions<CognitiveTranslateApiOption>().Bind(configuration.GetSection("CognitiveTranslate"));
            services.AddOptions<EmailClientOption>().Bind(configuration.GetSection("EmailClient"));

            return services;
        }
    }
}
