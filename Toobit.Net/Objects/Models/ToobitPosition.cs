using System.Text.Json.Serialization;
using Toobit.Net.Enums;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Position info
    /// </summary>
    public record ToobitPosition
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
        /// Average entry price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// Position
        /// </summary>
        [JsonPropertyName("position")]
        public decimal Position { get; set; }
        /// <summary>
        /// Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Last price
        /// </summary>
        [JsonPropertyName("lastPrice")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Position value
        /// </summary>
        [JsonPropertyName("positionValue")]
        public decimal PositionValue { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonPropertyName("flp")]
        public decimal? LiquidationPrice { get; set; }
        /// <summary>
        /// Margin
        /// </summary>
        [JsonPropertyName("margin")]
        public decimal Margin { get; set; }
        /// <summary>
        /// Margin rate
        /// </summary>
        [JsonPropertyName("marginRate")]
        public decimal? MarginRate { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealizedPnL")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Profit rate
        /// </summary>
        [JsonPropertyName("profitRate")]
        public decimal ProfitRate { get; set; }
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [JsonPropertyName("realizedPnL")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// Max notional value
        /// </summary>
        [JsonPropertyName("maxNotionalValue")]
        public decimal MaxNotionalValue { get; set; }
    }


}
