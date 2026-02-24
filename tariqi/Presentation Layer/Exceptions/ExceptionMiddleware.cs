using System.Net;
using System.Text.Json;
using tariqi.Application_Layer.Exceptions;
using tariqi.Presentation_Layer.Responses;

namespace tariqi.Presentation_Layer.Exceptions
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            var traceId = context.TraceIdentifier;

            context.Response.ContentType = "application/json";

            var (statusCode, message) = exception switch
            {
                NotFoundException =>
                    (HttpStatusCode.NotFound, exception.Message),

                DomainValidationException =>
                    (HttpStatusCode.BadRequest, exception.Message),

                UnauthorizedException =>
                    (HttpStatusCode.Unauthorized, exception.Message),

                _ =>
                    (HttpStatusCode.InternalServerError,
                     "An unexpected error occurred.")
            };

            context.Response.StatusCode = (int)statusCode;

            // Logging
            if (statusCode == HttpStatusCode.InternalServerError)
            {
                _logger.LogError(exception,
                    "Unhandled Exception | TraceId: {TraceId}",
                    traceId);
            }
            else
            {
                _logger.LogWarning(exception,
                    "Handled Exception | TraceId: {TraceId}",
                    traceId);
            }

            var response = ApiResponseFactory.Fail<object>(
                message,
                traceId: traceId
            );

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}
