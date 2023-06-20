using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace Rich_Domain_Model_Demo.Exceptions
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {

                await HandleExceptionAsync(httpContext, e);
            }
        }
        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            //Log The Exception
            string result = JsonConvert.SerializeObject(exception.Message);

            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return httpContext.Response.WriteAsync(result);
        }
    }
}
