using System.Net;
using System.Text.Json.Serialization;

using WssConsultingTask.Application.Exceptions;

namespace WssConsultingTask.API.Middlewares
{
    public static class ExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }

    public class ExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlerMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (WssApplicationException ex)
            {
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = ex.ApplicationErrorCode,
                    Message = ex.Message,
                };

                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, errorResponse);

                logger.LogError(ex, errorResponse.Message);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse
                {
                    Message = "An internal error occurred in the Application.",
                };

                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, errorResponse);

                logger.LogError(ex, errorResponse.Message);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, ErrorResponse errorResponse)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(errorResponse);
        }

        private class ErrorResponse
        {
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public ApplicationErrorCode? ErrorCode { get; set; }

            public string? Message { get; set; }
        }
    }
}
