using System;
using System.Text.Json.Serialization;
using Toobit.Net.Enums;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Order info
    /// </summary>
    public record ToobitOrder
    {
        /// <summary>
        /// ["<c>accountId</c>"] Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbolName</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbolName")]
        public string SymbolName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// The average price the order was filled
        /// </summary>
        public decimal? AverageFillPrice
        {
            get
            {
                if (QuantityFilled == 0 || QuoteQuantityFilled == 0)
                    return null;

                return QuoteQuantityFilled / QuantityFilled;
            }
        }

        /// <summary>
        /// ["<c>origQty</c>"] Quantity
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>executedQty</c>"] Quantity filled
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>cumulativeQuoteQty</c>"] Quote quantity filled
        /// </summary>
        [JsonPropertyName("cumulativeQuoteQty")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>timeInForce</c>"] Time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>transactTime</c>"] Transact time
        /// </summary>
        [JsonPropertyName("transactTime")]
        public DateTime? TransactTime { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Create time
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>isWorking</c>"] Is working
        /// </summary>
        [JsonPropertyName("isWorking")]
        public bool IsWorking { get; set; }
    }


}
