using FluentValidation;
using System.Net;
using System.Text.Json;

namespace API.Middlewares
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

                var result = JsonSerializer.Serialize(new { Errors = errors });

                await httpContext.Response.WriteAsync(result);
            }
        }
    }

    public static class ValidationExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseValidationExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidationExceptionMiddleware>();
        }
    }
}
