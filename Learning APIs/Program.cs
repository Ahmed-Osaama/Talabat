using Learning_APIs.Extensions;

namespace Learning_APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            #region DI
            var builder = WebApplication.CreateBuilder(args);
            #region WebApiServices
            builder.Services.AddWebApiServices(builder.Configuration);
            #endregion

            #region InfrastructureServices
            builder.Services.AddInfrastructureServices(builder.Configuration);
            #endregion

            #region CoreServices
            builder.Services.AddCoreServices(builder.Configuration);
            #endregion 
            #endregion

            #region Pipelines - Middlewares
            var app = builder.Build();
            #region SeedData
            await app.AddSeedDataAsync();
            #endregion

            #region ExceptionsMiddleware
            app.AddMiddlewares();
            #endregion
            // Configure the HTTP request pipeline.
            #region Swagger
            app.AddSwagger();
            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}
