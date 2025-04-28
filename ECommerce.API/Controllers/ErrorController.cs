using ECommerce.API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("errors/{statusCode}")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error(int statusCode) 
        {
            return new ObjectResult(new ResponseAPI(statusCode));
        }
    }
}
