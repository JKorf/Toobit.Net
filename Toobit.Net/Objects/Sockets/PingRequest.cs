using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Toobit.Net.Objects.Sockets
{
    internal record PingRequest
    {
        [JsonPropertyName("ping")]
        public long Ping { get; set; }
    }
}
