using System;
using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Position isolated margin
    /// </summary>
    public record ToobitPositionMargin
    {
        /// <summary>
        /// ["<c>code</c>"] Code
        /// </summary>
        [JsonPropertyName("code")]
        public decimal Code { get; set; }
        /// <summary>
        /// ["<c>msg</c>"] Message
        /// </summary>
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>margin</c>"] Margin
        /// </summary>
        [JsonPropertyName("margin")]
        public decimal Margin { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }


}
