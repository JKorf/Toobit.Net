using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Toobit.Net.Enums;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Position update
    /// </summary>
    public record ToobitPositionUpdate
    {
        /// <summary>
        /// Event
        /// </summary>
        [JsonPropertyName("e")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// EventTime
        /// </summary>
        [JsonPropertyName("E")]
        public DateTime EventTime { get; set; }
        /// <summary>
        /// Account id
        /// </summary>
        [JsonPropertyName("A")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("S")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// Average entry price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// Position quantity
        /// </summary>
        [JsonPropertyName("P")]
        public decimal PositionQuantity { get; set; }
        /// <summary>
        /// Positions available
        /// </summary>
        [JsonPropertyName("a")]
        public decimal AvailablePositions { get; set; }
        /// <summary>
        /// LiquidationPrice
        /// </summary>
        [JsonPropertyName("f")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// Position margin
        /// </summary>
        [JsonPropertyName("m")]
        public decimal Margin { get; set; }
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [JsonPropertyName("r")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// Margin typ
        /// </summary>
        [JsonPropertyName("mt")]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// RiskRate
        /// </summary>
        [JsonPropertyName("rr")]
        public decimal RiskRate { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("up")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Profit rate
        /// </summary>
        [JsonPropertyName("pr")]
        public decimal ProfitRate { get; set; }
        /// <summary>
        /// Position value
        /// </summary>
        [JsonPropertyName("pv")]
        public decimal PositionValue { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Leverage { get; set; }
    }


}
