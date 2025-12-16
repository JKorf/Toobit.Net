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
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Bids list
        /// </summary>
        [JsonPropertyName("b")]
        public ToobitOrderBookEntry[] Bids { get; set; } = [];
        /// <summary>
        /// Asks list
        /// </summary>
        [JsonPropertyName("a")]
        public ToobitOrderBookEntry[] Asks { get; set; } = [];
    }
}
