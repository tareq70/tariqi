using System.Text.Json.Serialization;

namespace tariqi.Presentation_Layer.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public object? Errors { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public object? Meta { get; set; }

        public string? TraceId { get; set; }
    }
}
