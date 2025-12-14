using LoanAPI.Application; 
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace FinalW2.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILoggerService logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, logger);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, ILoggerService logger)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Scoped service injected safely via InvokeAsync
            await logger.LogErrorAsync($"Unhandled exception: {exception.Message}", exception);

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error"
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

}
