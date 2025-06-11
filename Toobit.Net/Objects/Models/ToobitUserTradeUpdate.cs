using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Toobit.Net.Enums;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// User trade update
    /// </summary>
    public record ToobitUserTradeUpdate
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
        /// Quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("T")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OrderId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("c")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Account id
        /// </summary>
        [JsonPropertyName("a")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// Is maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
    }


}
