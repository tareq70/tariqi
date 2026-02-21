namespace tariqi.Presentation_Layer.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        public T? Data { get; set; }

        public object? Errors { get; set; }

        public object? Meta { get; set; }

        public string? TraceId { get; set; }
    }
}
