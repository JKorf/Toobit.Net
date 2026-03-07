using System;
using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Mark price
    /// </summary>
    public record ToobitMarkPrice
    {
        /// <summary>
        /// ["<c>exchangeId</c>"] Exchange id
        /// </summary>
        [JsonPropertyName("exchangeId")]
        public long ExchangeId { get; set; }
        /// <summary>
        /// ["<c>symbolId</c>"] Symbol id
        /// </summary>
        [JsonPropertyName("symbolId")]
        public string SymbolId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }


}
