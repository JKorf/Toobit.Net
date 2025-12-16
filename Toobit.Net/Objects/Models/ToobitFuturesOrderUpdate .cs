using System;
using System.Text.Json.Serialization;
using Toobit.Net.Enums;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Futures order update
    /// </summary>
    public record ToobitFuturesOrderUpdate
    {
        /// <summary>
        /// Event
        /// </summary>
        [JsonPropertyName("e")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// Event time
        /// </summary>
        [JsonPropertyName("E")]
        public DateTime EventTime { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("c")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Is a close order
        /// </summary>
        [JsonPropertyName("C")]
        public bool IsCloseOrder { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("o")]
        public FuturesUpdateOrderType OrderType { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("f")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Order quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// Price type
        /// </summary>
        [JsonPropertyName("pt")]
        public PriceType PriceType { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("X")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("i")]
        public long OrderId { get; set; }
        /// <summary>
        /// Last filled quantity
        /// </summary>
        [JsonPropertyName("l")]
        public decimal? LastFillQuantity { get; set; }
        /// <summary>
        /// Quantity filled
        /// </summary>
        [JsonPropertyName("z")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Last filled price
        /// </summary>
        [JsonPropertyName("L")]
        public decimal? LastFillPrice { get; set; }
        /// <summary>
        /// Last trade id
        /// </summary>
        [JsonPropertyName("td")]
        public long? LastTradeId { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("n")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("N")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// U
        /// </summary>
        [JsonPropertyName("u")]
        public bool U { get; set; }
        /// <summary>
        /// Is working
        /// </summary>
        [JsonPropertyName("w")]
        public bool IsWorking { get; set; }
        /// <summary>
        /// Is maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// Margin type
        /// </summary>
        [JsonPropertyName("mt")]
        public MarginType? MarginType { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("O")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Quantity filled in quote asset
        /// </summary>
        [JsonPropertyName("Z")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// UpdateTime
        /// </summary>
        [JsonPropertyName("U")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [JsonPropertyName("rp")]
        public decimal? RealizedPnl { get; set; }
    }


}
