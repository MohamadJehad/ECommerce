using ECommerce.API.Helper;
using System.Net;
using System.Text.Json;

namespace ECommerce.API.Middleware
{
    public class ExceptionMeddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMeddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task TaskAsync(HttpContext context)
        {
            try
            {
                await _next(context);
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
    }
}
