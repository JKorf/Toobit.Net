using System;
using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    internal record ToobitServerTime
    {
        [JsonPropertyName("serverTime")]
        public DateTime Timestamp { get; set; }
    }
}
