using System;
using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// User trade info
    /// </summary>
    public record ToobitUserTrade
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
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
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>matchOrderId</c>"] Match order id
        /// </summary>
        [JsonPropertyName("matchOrderId")]
        public long MatchOrderId { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>qty</c>"] Quantity
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>commission</c>"] Fee
        /// </summary>
        [JsonPropertyName("commission")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>commissionAsset</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("commissionAsset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>isBuyer</c>"] Is buyer
        /// </summary>
        [JsonPropertyName("isBuyer")]
        public bool IsBuyer { get; set; }
        /// <summary>
        /// ["<c>isMaker</c>"] Is maker
        /// </summary>
        [JsonPropertyName("isMaker")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public ToobitUserTradeFee FeeInfo { get; set; } = null!;
        /// <summary>
        /// ["<c>feeCoinId</c>"] Fee asset id
        /// </summary>
        [JsonPropertyName("feeCoinId")]
        public string FeeAssetId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>feeAmount</c>"] Fee quantity
        /// </summary>
        [JsonPropertyName("feeAmount")]
        public decimal FeeQuantity { get; set; }
        /// <summary>
        /// ["<c>makerRebate</c>"] Maker rebate
        /// </summary>
        [JsonPropertyName("makerRebate")]
        public decimal MakerRebate { get; set; }
        /// <summary>
        /// ["<c>ticketId</c>"] Ticket id
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
        /// ["<c>feeCoinId</c>"] Fee asset id
        /// </summary>
        [JsonPropertyName("feeCoinId")]
        public string FeeAssetId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>feeCoinName</c>"] Fee asset name
        /// </summary>
        [JsonPropertyName("feeCoinName")]
        public string FeeAssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
    }


}
