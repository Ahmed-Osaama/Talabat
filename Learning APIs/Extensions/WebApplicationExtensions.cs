using Domain.Contracts;
using Learning_APIs.Middlewares;

namespace Learning_APIs.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> AddSeedDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await dataSeeding.SeedDataAsync();
            await dataSeeding.SeedIdentityDataAsync();
            return app;
        }
        public static WebApplication AddMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            return app;

        }
        public static WebApplication AddSwagger(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            return app;
        }
    }
}
