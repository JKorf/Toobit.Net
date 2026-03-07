using System;
using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Index price update
    /// </summary>
    public record ToobitIndexUpdate
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>index</c>"] Index price
        /// </summary>
        [JsonPropertyName("index")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// ["<c>edp</c>"] Average of indices over 10 minutes
        /// </summary>
        [JsonPropertyName("edp")]
        public decimal IndicesAverage { get; set; }
        /// <summary>
        /// ["<c>formula</c>"] Formula
        /// </summary>
        [JsonPropertyName("formula")]
        public string Formula { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }
}
