using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Futures balance info
    /// </summary>
    public record ToobitFuturesBalance
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Total balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal TotalBalance { get; set; }
        /// <summary>
        /// Available balance Include unrealized profit and loss
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// Position margin
        /// </summary>
        [JsonPropertyName("positionMargin")]
        public decimal PositionMargin { get; set; }
        /// <summary>
        /// Order margin
        /// </summary>
        [JsonPropertyName("orderMargin")]
        public decimal OrderMargin { get; set; }
        /// <summary>
        /// Cross unrealized profit and loss
        /// </summary>
        [JsonPropertyName("crossUnRealizedPnl")]
        public decimal CrossUnrealizedPnl { get; set; }
    }


}
