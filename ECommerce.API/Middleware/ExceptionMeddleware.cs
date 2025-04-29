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

            }
        }
    }
}
