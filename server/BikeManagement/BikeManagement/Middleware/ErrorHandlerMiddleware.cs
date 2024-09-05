using BikeManagement.Helpers;
using BikeManagement.Responses;
using Serilog;
using System.Net;

namespace BikeManagement.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ApiResponse<object>(
                false,
                Constants.RESPONSE_INTERNAL_SERVER,
                null!,
                context.Response.StatusCode);

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
