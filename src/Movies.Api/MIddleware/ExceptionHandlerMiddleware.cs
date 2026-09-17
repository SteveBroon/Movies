using System.Net;

namespace Movies.Api.MIddleware
{
    public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        private readonly ILogger _logger = logger;
        private readonly RequestDelegate next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An unhandled exception occurred while processing the request.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("An internal error occurred.");
            }
        }
    }
}
