using System;
using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{/// <summary>
 /// 
 /// </summary>
    public record ToobitWithdrawal
    {
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>id </c>"] Id
        /// </summary>
        [JsonPropertyName("id ")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>accountId</c>"] Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>coinId </c>"] Asset id
        /// </summary>
        [JsonPropertyName("coinId ")]
        public string AssetId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>coinName</c>"] Asset name
        /// </summary>
        [JsonPropertyName("coinName")]
        public string AssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>address</c>"] Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>addressExt</c>"] Address tag
        /// </summary>
        [JsonPropertyName("addressExt")]
        public string Tag { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quantity</c>"] Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>arriveQuantity</c>"] Arrive quantity
        /// </summary>
        [JsonPropertyName("arriveQuantity")]
        public decimal ArriveQuantity { get; set; }
        /// <summary>
        /// ["<c>statusCode</c>"] Withdrawal status code
        /// </summary>
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Withdrawal status
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }
        /// <summary>
        /// ["<c>txId </c>"] Transaction id 
        /// </summary>
        [JsonPropertyName("txId ")]
        public string? TransactionId { get; set; }
        /// <summary>
        /// ["<c>txIdUrl </c>"] Transaction id url 
        /// </summary>
        [JsonPropertyName("txIdUrl ")]
        public string? TransactionIdUrl { get; set; }
        /// <summary>
        /// ["<c>walletHandleTime</c>"] Wallet handle time
        /// </summary>
        [JsonPropertyName("walletHandleTime")]
        public DateTime? WalletHandleTime { get; set; }
        /// <summary>
        /// ["<c>feeCoinId </c>"] Fee asset id 
        /// </summary>
        [JsonPropertyName("feeCoinId ")]
        public string FeeAssetId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>feeCoinName </c>"] Fee asset name 
        /// </summary>
        [JsonPropertyName("feeCoinName ")]
        public string FeeAssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>requiredConfirmTimes </c>"] Required confirmation times 
        /// </summary>
        [JsonPropertyName("requiredConfirmTimes ")]
        public int? RequiredConfirmTimes { get; set; }
        /// <summary>
        /// ["<c>confirmTimes </c>"] Confirm times 
        /// </summary>
        [JsonPropertyName("confirmTimes ")]
        public int? ConfirmTimes { get; set; }
        /// <summary>
        /// ["<c>kernelId</c>"] Kernel id
        /// </summary>
        [JsonPropertyName("kernelId")]
        public string? KernelId { get; set; }
        /// <summary>
        /// ["<c>isInternalTransfer</c>"] Is internal transfer
        /// </summary>
        [JsonPropertyName("isInternalTransfer")]
        public bool IsInternalTransfer { get; set; }
    }


}
