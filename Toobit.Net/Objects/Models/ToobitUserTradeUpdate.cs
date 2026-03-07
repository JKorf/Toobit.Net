using System;
using System.Text.Json.Serialization;
using Toobit.Net.Enums;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// User trade update
    /// </summary>
    public record ToobitUserTradeUpdate
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
        /// ["<c>q</c>"] Quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>p</c>"] Price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>T</c>"] Trade id
        /// </summary>
        [JsonPropertyName("T")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>o</c>"] Order id
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OrderId { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Client order id
        /// </summary>
        [JsonPropertyName("c")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>a</c>"] Account id
        /// </summary>
        [JsonPropertyName("a")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>m</c>"] Is maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// ["<c>S</c>"] Side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
    }


}
