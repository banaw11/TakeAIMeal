using Microsoft.EntityFrameworkCore;
using TakeAIMeal.API.Services.Interfaces;
using TakeAIMeal.API.Services.Logic;
using TakeAIMeal.Common.Services.Interfaces;
using TakeAIMeal.Common.Services.Logic;
using TakeAIMeal.Data;

namespace TakeAIMeal.API.Extensions
{
    public static class ApplicationDIContainerExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ITakeAIMealDbContext, TakeAIMealDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TakeAIMealDbConnection"));
            });

            services.AddScoped<IBlobStorageService, BlobStorageService>(provider => new BlobStorageService(configuration.GetConnectionString("StorageAccount")));
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ITextGeneratorService, TextGeneratorService>();
            services.AddScoped<ITextRecognitionService, TextRecognitionService>();
            services.AddScoped<ITranslateService, TranslateService>();
            services.AddScoped<ITipsService, TipsService>();
            services.AddScoped<IRecipeService, RecipeService>();

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            return services;
        }
    }
}
