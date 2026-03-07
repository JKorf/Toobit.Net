using System.Text.Json.Serialization;
using Toobit.Net.Enums;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Trading stop info
    /// </summary>
    public record ToobitTradingStop
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Position side
        /// </summary>
        [JsonPropertyName("side")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>takeProfit</c>"] Take profit price
        /// </summary>
        [JsonPropertyName("takeProfit")]
        public decimal? TakeProfitPrice { get; set; }
        /// <summary>
        /// ["<c>stopLoss</c>"] Stop loss price
        /// </summary>
        [JsonPropertyName("stopLoss")]
        public decimal? StopLossPrice { get; set; }
        /// <summary>
        /// ["<c>tpTriggerBy</c>"] Tp trigger by
        /// </summary>
        [JsonPropertyName("tpTriggerBy")]
        public TriggerType? TakeProfitTriggerType { get; set; }
        /// <summary>
        /// ["<c>slTriggerBy</c>"] Sl trigger by
        /// </summary>
        [JsonPropertyName("slTriggerBy")]
        public TriggerType? StopLossTriggerType { get; set; }
    }


}
