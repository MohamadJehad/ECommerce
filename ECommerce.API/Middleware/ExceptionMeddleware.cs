using ECommerce.API.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace ECommerce.API.Middleware
{
    public class ExceptionMeddleware
    {
        private readonly RequestDelegate next;
        private readonly IHostEnvironment environment;
        private readonly IMemoryCache memoryCache;
        private readonly TimeSpan rateLimitWindow = TimeSpan.FromSeconds(30);

        public ExceptionMeddleware(RequestDelegate next, IHostEnvironment environment, IMemoryCache memoryCache)
        {
            this.next = next;
            this.environment = environment;
            this.memoryCache = memoryCache;
        }

        public async Task TaskAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";
                var response = new ApiException(statusCode, ex.Message, ex.StackTrace);
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }

        private bool IsRequestAllowed (HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress.ToString();
            var cacheKey = $"Rate:{ip}";
            var dateNow = DateTime.Now;

            var (timesTamp, count) = memoryCache.GetOrCreate(cacheKey, entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = rateLimitWindow;
                    return (timesTamp: dateNow, count: 0);
                });

            if (dateNow - timesTamp < rateLimitWindow)
            {
                if (count >= 8)
                {
                    return false;
                }

                memoryCache.Set(cacheKey, (timesTamp, count += 1), rateLimitWindow);
            }
            else
            {
                memoryCache.Set(cacheKey, (timesTamp, count), rateLimitWindow);
            }

            return true;
        }
    }
}
