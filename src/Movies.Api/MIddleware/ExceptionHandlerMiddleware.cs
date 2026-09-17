using System.Net;

namespace Movies.Api.MIddleware;

public class ExceptionHandlerMiddleware
{
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;
    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred while processing the request.");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync("An internal error occurred.");
        }
    }
}
