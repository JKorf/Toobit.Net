using System;
using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Mark price update
    /// </summary>
    public record ToobitMarkPriceUpdate
    {
        /// <summary>
        /// ["<c>exchangeId</c>"] Exchange id
        /// </summary>
        [JsonPropertyName("exchangeId")]
        public long ExchangeId { get; set; }
        /// <summary>
        /// ["<c>symbolId</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbolId")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] Mark price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }
}
