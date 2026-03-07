using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Ticker price info
    /// </summary>
    public record ToobitTickerUpdate
    {
        /// <summary>
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public decimal Timestamp { get; set; }
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>c</c>"] Last trade price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// ["<c>o</c>"] Open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal? OpenPrice { get; set; }
        /// <summary>
        /// ["<c>h</c>"] High price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Volume in base asset
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>qv</c>"] Volume in quote asset
        /// </summary>
        [JsonPropertyName("qv")]
        public decimal QuoteVolume { get; set; }
        /// <summary>
        /// ["<c>m</c>"] Margin
        /// </summary>
        [JsonPropertyName("m")]
        public decimal Margin { get; set; }
        /// <summary>
        /// ["<c>e</c>"] Last trade id
        /// </summary>
        [JsonPropertyName("e")]
        public long LastTradeId { get; set; }
    }


}
