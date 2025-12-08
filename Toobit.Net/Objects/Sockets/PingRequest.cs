using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Sockets
{
    internal record PingRequest
    {
        [JsonPropertyName("ping")]
        public long Ping { get; set; }
    }
}
