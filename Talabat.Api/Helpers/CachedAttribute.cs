using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.services;

namespace Talabat.Api.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int timeToLiveSeconds;

        public CachedAttribute(int TimeToLiveSeconds)
        {
            timeToLiveSeconds = TimeToLiveSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CachedService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var cachedKey = GetCachedKey(context.HttpContext.Request);
            var cachedResponse = await CachedService.GetResponseCacheAsync(cachedKey);
            if(!string.IsNullOrEmpty(cachedResponse))
            {
                var ContentResult = new ContentResult()
                { 
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = ContentResult;
                return;
            }
           var OkResult =  await next.Invoke();
           if(OkResult.Result is OkObjectResult okObjectResult)
            {
                await CachedService.ResponseCacheAsync(cachedKey, okObjectResult.Value, TimeSpan.FromSeconds(timeToLiveSeconds));
            }
        }

        private string GetCachedKey(HttpRequest request)
        {
            var CacheKeyString = new StringBuilder();
            CacheKeyString.Append(request.Path);
            foreach(var (key,value) in request.Query.OrderBy(x => x.Key))
            CacheKeyString.Append($"|{key}-{value}");
            return CacheKeyString.ToString();
        }
    }
}
