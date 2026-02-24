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
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
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

            var (statusCode, message, errorType) = exception switch
            {
                NotFoundException =>
                    (HttpStatusCode.NotFound, exception.Message, "NotFound"),

                DomainValidationException =>
                    (HttpStatusCode.BadRequest, exception.Message, "Validation"),

                UnauthorizedException =>
                    (HttpStatusCode.Unauthorized, exception.Message, "Unauthorized"),

                _ =>
                    (HttpStatusCode.InternalServerError,
                     "An unexpected error occurred.", "InternalServerError")
            };

            if(_environment.IsDevelopment())
            {
                message = exception.Message;
            }

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
                errorType,
                traceId: traceId
            );

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}
