using Newtonsoft.Json;
using System.Net;

namespace TaskMaster.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // This method is called by the ASP.NET Core runtime
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Call the next middleware in the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExeptionAsync(context, ex);
            }
        }

        private async Task HandleExeptionAsync(HttpContext context, Exception exception)
        {
            var code = exception switch
            {
                KeyNotFoundException => HttpStatusCode.NotFound,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError,
            };

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            // Log the error with the response status code
            _logger.LogError(exception, "An unhandled exception has occurred while executing the request. Response StatusCode: {StatusCode}", context.Response.StatusCode);

            await context.Response.WriteAsync(result);

        }  
    }
}
