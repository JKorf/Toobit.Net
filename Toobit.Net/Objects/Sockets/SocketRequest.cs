using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Sockets
{
    internal record SocketRequest
    {
        [JsonPropertyName("symbol")]
        public string? Symbols { get; set; }
        [JsonPropertyName("topic")]
        public string Topic { get; set; } = string.Empty;
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;
        [JsonPropertyName("params")]
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }
}
