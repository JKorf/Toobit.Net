using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Toobit.Net.Objects.Models
{
    internal record ToobitServerTime
    {
        [JsonPropertyName("serverTime")]
        public DateTime Timestamp { get; set; }
    }
}
