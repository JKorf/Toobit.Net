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
        /// ["<c>avgPrice</c>"] Average entry price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// ["<c>position</c>"] Position
        /// </summary>
        [JsonPropertyName("position")]
        public decimal Position { get; set; }
        /// <summary>
        /// ["<c>available</c>"] Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>lastPrice</c>"] Last price
        /// </summary>
        [JsonPropertyName("lastPrice")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>positionValue</c>"] Position value
        /// </summary>
        [JsonPropertyName("positionValue")]
        public decimal PositionValue { get; set; }
        /// <summary>
        /// ["<c>flp</c>"] Liquidation price
        /// </summary>
        [JsonPropertyName("flp")]
        public decimal? LiquidationPrice { get; set; }
        /// <summary>
        /// ["<c>margin</c>"] Margin
        /// </summary>
        [JsonPropertyName("margin")]
        public decimal Margin { get; set; }
        /// <summary>
        /// ["<c>marginRate</c>"] Margin rate
        /// </summary>
        [JsonPropertyName("marginRate")]
        public decimal? MarginRate { get; set; }
        /// <summary>
        /// ["<c>unrealizedPnL</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealizedPnL")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>profitRate</c>"] Profit rate
        /// </summary>
        [JsonPropertyName("profitRate")]
        public decimal ProfitRate { get; set; }
        /// <summary>
        /// ["<c>realizedPnL</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("realizedPnL")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>maxNotionalValue</c>"] Max notional value
        /// </summary>
        [JsonPropertyName("maxNotionalValue")]
        public decimal MaxNotionalValue { get; set; }
    }


}
