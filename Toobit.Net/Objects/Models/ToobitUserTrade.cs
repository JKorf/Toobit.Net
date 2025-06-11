using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// User trade info
    /// </summary>
    public record ToobitUserTrade
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("symbolName")]
        public string SymbolName { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// Match order id
        /// </summary>
        [JsonPropertyName("matchOrderId")]
        public long MatchOrderId { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("commission")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("commissionAsset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Is buyer
        /// </summary>
        [JsonPropertyName("isBuyer")]
        public bool IsBuyer { get; set; }
        /// <summary>
        /// Is maker
        /// </summary>
        [JsonPropertyName("isMaker")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public ToobitUserTradeFee FeeInfo { get; set; } = null!;
        /// <summary>
        /// Fee asset id
        /// </summary>
        [JsonPropertyName("feeCoinId")]
        public string FeeAssetId { get; set; } = string.Empty;
        /// <summary>
        /// Fee quantity
        /// </summary>
        [JsonPropertyName("feeAmount")]
        public decimal FeeQuantity { get; set; }
        /// <summary>
        /// Maker rebate
        /// </summary>
        [JsonPropertyName("makerRebate")]
        public decimal MakerRebate { get; set; }
        /// <summary>
        /// Ticket id
        /// </summary>
        [JsonPropertyName("ticketId")]
        public long TicketId { get; set; }
    }

    /// <summary>
    /// Fee info
    /// </summary>
    public record ToobitUserTradeFee
    {
        /// <summary>
        /// Fee asset id
        /// </summary>
        [JsonPropertyName("feeCoinId")]
        public string FeeAssetId { get; set; } = string.Empty;
        /// <summary>
        /// Fee asset name
        /// </summary>
        [JsonPropertyName("feeCoinName")]
        public string FeeAssetName { get; set; } = string.Empty;
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
    }


}
