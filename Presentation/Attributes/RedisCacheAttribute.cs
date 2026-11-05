using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServicesAbstraction;
using System.Text;

namespace Presentation.Attributes
{
    public class RedisCacheAttribute(int DurationInSeconds = 120) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;
            string Key = GenerateKey(context.HttpContext.Request);
            var result =await cacheService.GetCachedAsync(Key);
            if (result != null)
            {
                context.Result = new ContentResult
                {
                    Content = result,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                return;
            }
            var resultContext = await next.Invoke();
            if (resultContext != null)
            {
                if (resultContext.Result is ObjectResult objectResult)
                {
                    await cacheService.SetCacheAsync(Key, objectResult.Value!, TimeSpan.FromMinutes(DurationInSeconds));
                }
            }
        }
        private string GenerateKey(HttpRequest request)
        {
            var Key = new StringBuilder();
            Key.Append(request.Path);
            foreach(var item in request.Query.OrderBy(x => x.Key))
            {
                Key.Append($"{item.Key}:{item.Value}");
            }
            return Key.ToString();
        }
    }
}
