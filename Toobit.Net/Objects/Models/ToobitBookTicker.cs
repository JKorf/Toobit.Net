using System;
using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Book ticker
    /// </summary>
    public record ToobitBookTicker
    {
        /// <summary>
        /// ["<c>t</c>"] Timestamp of the data
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>b</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("b")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>bq</c>"] Best bid quantity
        /// </summary>
        [JsonPropertyName("bq")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>a</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("a")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>aq</c>"] Best ask quantity
        /// </summary>
        [JsonPropertyName("aq")]
        public decimal? BestAskQuantity { get; set; }
    }


}
