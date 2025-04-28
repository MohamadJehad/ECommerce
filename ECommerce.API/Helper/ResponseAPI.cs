namespace ECommerce.API.Helper
{
    public class ResponseAPI
    {
        public ResponseAPI(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? getMessageFromStatusCode(statusCode);
        }
        private string? getMessageFromStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Done",
                400 => "Bad Request",
                401 => "Un Authorized",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => null,
            };
        }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
