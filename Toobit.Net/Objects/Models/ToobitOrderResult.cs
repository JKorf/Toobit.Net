using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Order result
    /// </summary>
    internal record ToobitOrderResult
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("msg")]
        public string? Message { get; set; }

        [JsonPropertyName("order")]
        public ToobitOrder? Order { get; set; }
    }
}
