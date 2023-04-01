using TakeAIMeal.Common.Options;

namespace TakeAIMeal.API.Extensions
{
    public static class ApplicationOptionsExtension
    {
        public static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<OpentAIApiOption>().Bind(configuration.GetSection("OpenAI"));
            services.AddOptions<CognitiveLanguageApiOption>().Bind(configuration.GetSection("CognitiveLanguage"));

            return services;
        }
    }
}
