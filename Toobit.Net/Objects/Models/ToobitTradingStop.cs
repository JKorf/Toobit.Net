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
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("side")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// Take profit price
        /// </summary>
        [JsonPropertyName("takeProfit")]
        public decimal? TakeProfitPrice { get; set; }
        /// <summary>
        /// Stop loss price
        /// </summary>
        [JsonPropertyName("stopLoss")]
        public decimal? StopLossPrice { get; set; }
        /// <summary>
        /// Tp trigger by
        /// </summary>
        [JsonPropertyName("tpTriggerBy")]
        public TriggerType? TakeProfitTriggerType { get; set; }
        /// <summary>
        /// Sl trigger by
        /// </summary>
        [JsonPropertyName("slTriggerBy")]
        public TriggerType? StopLossTriggerType { get; set; }
    }


}
