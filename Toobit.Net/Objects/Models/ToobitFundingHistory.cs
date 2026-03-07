using System;
using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Funding rate history
    /// </summary>
    public record ToobitFundingHistory
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>settleTime</c>"] Funding time
        /// </summary>
        [JsonPropertyName("settleTime")]
        public DateTime FundingTime { get; set; }
        /// <summary>
        /// ["<c>settleRate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("settleRate")]
        public decimal FundingRate { get; set; }
    }


}
