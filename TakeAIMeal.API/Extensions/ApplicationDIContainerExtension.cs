using Microsoft.EntityFrameworkCore;
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

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            return services;
        }
    }
}
