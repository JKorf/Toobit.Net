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
    /// Order request
    /// </summary>
    public record ToobitFuturesOrderRequest
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public FuturesOrderSide OrderSide { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("type")]
        public FuturesOrderType OrderType { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("timeInForce"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Limit price
        /// </summary>
        [JsonPropertyName("price"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Price { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("newClientOrderId")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Price type
        /// </summary>
        [JsonPropertyName("priceType")]
        public PriceType PriceType { get; set; }
    }
}
