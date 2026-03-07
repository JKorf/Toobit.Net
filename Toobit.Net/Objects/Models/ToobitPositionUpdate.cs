using System;
using System.Text.Json.Serialization;
using Toobit.Net.Enums;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Position update
    /// </summary>
    public record ToobitPositionUpdate
    {
        /// <summary>
        /// ["<c>e</c>"] Event
        /// </summary>
        [JsonPropertyName("e")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>E</c>"] EventTime
        /// </summary>
        [JsonPropertyName("E")]
        public DateTime EventTime { get; set; }
        /// <summary>
        /// ["<c>A</c>"] Account id
        /// </summary>
        [JsonPropertyName("A")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>S</c>"] Position side
        /// </summary>
        [JsonPropertyName("S")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>p</c>"] Average entry price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// ["<c>P</c>"] Position quantity
        /// </summary>
        [JsonPropertyName("P")]
        public decimal PositionQuantity { get; set; }
        /// <summary>
        /// ["<c>a</c>"] Positions available
        /// </summary>
        [JsonPropertyName("a")]
        public decimal AvailablePositions { get; set; }
        /// <summary>
        /// ["<c>f</c>"] LiquidationPrice
        /// </summary>
        [JsonPropertyName("f")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// ["<c>m</c>"] Position margin
        /// </summary>
        [JsonPropertyName("m")]
        public decimal Margin { get; set; }
        /// <summary>
        /// ["<c>r</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("r")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>mt</c>"] Margin typ
        /// </summary>
        [JsonPropertyName("mt")]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// ["<c>rr</c>"] RiskRate
        /// </summary>
        [JsonPropertyName("rr")]
        public decimal RiskRate { get; set; }
        /// <summary>
        /// ["<c>up</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("up")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>pr</c>"] Profit rate
        /// </summary>
        [JsonPropertyName("pr")]
        public decimal ProfitRate { get; set; }
        /// <summary>
        /// ["<c>pv</c>"] Position value
        /// </summary>
        [JsonPropertyName("pv")]
        public decimal PositionValue { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Leverage
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Leverage { get; set; }
    }


}
