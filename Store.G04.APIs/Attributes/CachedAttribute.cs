using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Store.G04.Core.Services.Contract;
using System.Text;

namespace Store.G04.APIs.Attributes
{
    public class CachedAttribute : Attribute  ,IAsyncActionFilter
    {
        private readonly int _expiredtime;

        public CachedAttribute(int expiredtime )
        {
            _expiredtime = expiredtime;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheServices = context.HttpContext.RequestServices.GetRequiredService<ICacheService>(); // create object from icacheservic

            var cacheKey = GenerateKeyFromRequests(context.HttpContext.Request);

            var cacheResponse =await cacheServices.GetCachekeyAsync(cacheKey);
            if (!string.IsNullOrEmpty(cacheResponse)) 
            {
                var cacheResult = new ContentResult()
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = cacheResult;
                return;
            }
            var excutedContext = await next();
            if(excutedContext.Result is OkObjectResult response) 
            {
                await cacheServices.SetCacheKeyAsync(cacheKey, response.Value, TimeSpan.FromSeconds(_expiredtime));
            }
        }
        private string GenerateKeyFromRequests(HttpRequest request) 
        {
            var cacheKey =new StringBuilder();
            cacheKey.Append($"{request.Path}");
            foreach(var (key,value) in request.Query.OrderBy(X=>X.Key)) 
            {
                cacheKey.Append($"|{key}-{value}");
            }
            return cacheKey.ToString();

        } 
     
    }
}
