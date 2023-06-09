﻿using Microsoft.EntityFrameworkCore;
using TakeAIMeal.API.Services.Interfaces;
using TakeAIMeal.API.Services.Logic;
using TakeAIMeal.Common.Services.Interfaces;
using TakeAIMeal.Common.Services.Logic;
using TakeAIMeal.Data;
using TakeAIMeal.Data.Repositories.Interfaces;
using TakeAIMeal.Data.Repositories.Logic;

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
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();


            services.AddScoped<IBlobStorageService, BlobStorageService>(provider => new BlobStorageService(configuration.GetConnectionString("StorageAccount")));
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ITextGeneratorService, TextGeneratorService>();
            services.AddScoped<ITextRecognitionService, TextRecognitionService>();
            services.AddScoped<ITranslateService, TranslateService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITipsService, TipsService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<ITemplateService, RazorViewTemplateService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IDictionarySerivce, DictionaryService>();
            services.AddScoped<IUserIdentityService, UserIdentityService>();
            services.AddScoped<IUserDietService, UserDietService>();

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IUserDietRepository, UserDietRepository>();
            services.AddScoped<IUserProductExclusionRepository, UserProductExclusionRepository>();
            services.AddScoped<IUserProductExclusionViewRepository, UserProductExclusionViewRepository>();

            return services;
        }
    }
}
