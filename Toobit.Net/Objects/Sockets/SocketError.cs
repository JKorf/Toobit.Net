using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Sockets
{
    internal record SocketError
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        [JsonPropertyName("desc")]
        public string Description { get; set; } = string.Empty;
    }
}
