using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
