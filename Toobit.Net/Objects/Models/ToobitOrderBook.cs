using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using System;
using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Order book info
    /// </summary>
    public record ToobitOrderBook
    {
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

    /// <summary>
    /// Order book entry
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<ToobitOrderBookEntry>))]
    public record ToobitOrderBookEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// Price
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }
    }
}
