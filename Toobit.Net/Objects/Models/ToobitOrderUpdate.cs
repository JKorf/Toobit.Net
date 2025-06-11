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
    /// Order update
    /// </summary>
    public record ToobitOrderUpdate
    {
        /// <summary>
        /// Event
        /// </summary>
        [JsonPropertyName("e")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// Event type
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
        /// Order side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide OrderSide { get; set; } 
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("o")]
        public OrderType OrderType { get; set; }
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
        /// Filled quantity
        /// </summary>
        [JsonPropertyName("z")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Last fill price
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
        /// Is working
        /// </summary>
        [JsonPropertyName("w")]
        public bool IsWorking { get; set; }
        /// <summary>
        /// Is maker side
        /// </summary>
        [JsonPropertyName("m")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("O")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Filled quote quantity
        /// </summary>
        [JsonPropertyName("Z")]
        public decimal QuoteQuantityFilled { get; set; }
    }


}
