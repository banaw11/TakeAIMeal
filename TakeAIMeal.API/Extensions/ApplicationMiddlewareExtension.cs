namespace TakeAIMeal.API.Extensions
{
    public static class ApplicationMiddlewareExtension
    {
        public static IApplicationBuilder UseResponseCacheOnSuccess(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() =>
                {
                    if (context.Response.StatusCode >= 400)
                    {
                        context.Response.Headers.Remove("Cache-Control");
                    }
                    return Task.FromResult(0);
                });
                await next();
            });

            return app;
        }
    }
}
