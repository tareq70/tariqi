using Microsoft.Identity.Client;

namespace tariqi.Presentation_Layer.Responses
{
    public static class ApiResponseFactory
    {
        public static ApiResponse<T> Success<T>(
            T data, string? message = null, object? meta = null, string? traceId = null)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message ?? "Operation is Success",
                Data = data,
                Meta = meta,
                TraceId = traceId
            };
        }

        public static ApiResponse<object> Success(
            string? message = null,
            string? traceId = null)
        {
            return new ApiResponse<object>
            {
                Success = true,
                Message = message,
                TraceId = traceId
            };
        }
        public static ApiResponse<T> Fail<T>(
            string? message = null, object? errors = null, string? traceId = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message ?? "Operation is Failed",
                Errors = errors,
                TraceId = traceId
            };
        }
    }
}
