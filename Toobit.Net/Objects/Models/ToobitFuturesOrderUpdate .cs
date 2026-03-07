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
        /// ["<c>e</c>"] Event
        /// </summary>
        [JsonPropertyName("e")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>E</c>"] Event time
        /// </summary>
        [JsonPropertyName("E")]
        public DateTime EventTime { get; set; }
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>c</c>"] Client order id
        /// </summary>
        [JsonPropertyName("c")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>C</c>"] Is a close order
        /// </summary>
        [JsonPropertyName("C")]
        public bool IsCloseOrder { get; set; }
        /// <summary>
        /// ["<c>S</c>"] Order side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>o</c>"] Order type
        /// </summary>
        [JsonPropertyName("o")]
        public FuturesUpdateOrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>f</c>"] Time in force
        /// </summary>
        [JsonPropertyName("f")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Order quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>p</c>"] Order price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>pt</c>"] Price type
        /// </summary>
        [JsonPropertyName("pt")]
        public PriceType PriceType { get; set; }
        /// <summary>
        /// ["<c>X</c>"] Order status
        /// </summary>
        [JsonPropertyName("X")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>i</c>"] Order id
        /// </summary>
        [JsonPropertyName("i")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Last filled quantity
        /// </summary>
        [JsonPropertyName("l")]
        public decimal? LastFillQuantity { get; set; }
        /// <summary>
        /// ["<c>z</c>"] Quantity filled
        /// </summary>
        [JsonPropertyName("z")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>L</c>"] Last filled price
        /// </summary>
        [JsonPropertyName("L")]
        public decimal? LastFillPrice { get; set; }
        /// <summary>
        /// ["<c>td</c>"] Last trade id
        /// </summary>
        [JsonPropertyName("td")]
        public long? LastTradeId { get; set; }
        /// <summary>
        /// ["<c>n</c>"] Fee
        /// </summary>
        [JsonPropertyName("n")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>N</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("N")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>u</c>"] U
        /// </summary>
        [JsonPropertyName("u")]
        public bool U { get; set; }
        /// <summary>
        /// ["<c>w</c>"] Is working
        /// </summary>
        [JsonPropertyName("w")]
        public bool IsWorking { get; set; }
        /// <summary>
        /// ["<c>m</c>"] Is maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// ["<c>mt</c>"] Margin type
        /// </summary>
        [JsonPropertyName("mt")]
        public MarginType? MarginType { get; set; }
        /// <summary>
        /// ["<c>O</c>"] Create time
        /// </summary>
        [JsonPropertyName("O")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>Z</c>"] Quantity filled in quote asset
        /// </summary>
        [JsonPropertyName("Z")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Leverage
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>U</c>"] UpdateTime
        /// </summary>
        [JsonPropertyName("U")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>rp</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("rp")]
        public decimal? RealizedPnl { get; set; }
    }


}
