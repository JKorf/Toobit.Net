using System;
using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Funding rate info
    /// </summary>
    public record ToobitFundingRate
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>nextFundingTime</c>"] Next funding time
        /// </summary>
        [JsonPropertyName("nextFundingTime")]
        public DateTime NextFundingTime { get; set; }
        /// <summary>
        /// ["<c>rate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("rate")]
        public decimal FundingRate { get; set; }
    }


}
