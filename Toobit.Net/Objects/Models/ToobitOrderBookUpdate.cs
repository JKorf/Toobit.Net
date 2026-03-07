using System;
using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Order book info
    /// </summary>
    public record ToobitOrderBookUpdate
    {
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>b</c>"] Bids list
        /// </summary>
        [JsonPropertyName("b")]
        public ToobitOrderBookEntry[] Bids { get; set; } = [];
        /// <summary>
        /// ["<c>a</c>"] Asks list
        /// </summary>
        [JsonPropertyName("a")]
        public ToobitOrderBookEntry[] Asks { get; set; } = [];
    }
}
