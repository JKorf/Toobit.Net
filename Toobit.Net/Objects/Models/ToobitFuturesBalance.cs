using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Futures balance info
    /// </summary>
    public record ToobitFuturesBalance
    {
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>balance</c>"] Total balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal TotalBalance { get; set; }
        /// <summary>
        /// ["<c>availableBalance</c>"] Available balance Include unrealized profit and loss
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// ["<c>positionMargin</c>"] Position margin
        /// </summary>
        [JsonPropertyName("positionMargin")]
        public decimal PositionMargin { get; set; }
        /// <summary>
        /// ["<c>orderMargin</c>"] Order margin
        /// </summary>
        [JsonPropertyName("orderMargin")]
        public decimal OrderMargin { get; set; }
        /// <summary>
        /// ["<c>crossUnRealizedPnl</c>"] Cross unrealized profit and loss
        /// </summary>
        [JsonPropertyName("crossUnRealizedPnl")]
        public decimal CrossUnrealizedPnl { get; set; }
    }


}
