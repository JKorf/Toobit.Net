using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Toobit.Net.Objects.Sockets
{
    internal record SocketUpdate<T>
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        [JsonPropertyName("symbolName")]
        public string SymbolName { get; set; } = string.Empty;
        [JsonPropertyName("topic")]
        public string Topic { get; set; } = string.Empty;
        [JsonPropertyName("params")]
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
        [JsonPropertyName("f")]
        public bool First { get; set; }
        [JsonPropertyName("sendTime")]
        public DateTime SendTime { get; set; }
    }
}
