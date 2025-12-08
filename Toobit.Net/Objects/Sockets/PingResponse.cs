using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Sockets
{
    internal record PingResponse
    {
        [JsonPropertyName("pong")]
        public long Pong { get; set; }
    }
}
