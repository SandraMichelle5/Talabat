using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.MiddelWares;

public class ExceptionMiddelWare
{
    private IHostEnvironment _env;
    private readonly RequestDelegate _next;
    private ILogger<ExceptionMiddelWare> _logger;
    public ExceptionMiddelWare(RequestDelegate next, ILogger<ExceptionMiddelWare>logger,IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;

    }
    public async Task InvokeAsync(HttpContext context)
            {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = _env.IsDevelopment()
                ? new ApiExceptionResponse(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                : new ApiExceptionResponse(context.Response.StatusCode, "Internal Server Error");
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
    }
}
