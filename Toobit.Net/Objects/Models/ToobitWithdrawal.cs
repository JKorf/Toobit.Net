using System;
using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{/// <summary>
 /// 
 /// </summary>
    public record ToobitWithdrawal
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id ")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// Asset id
        /// </summary>
        [JsonPropertyName("coinId ")]
        public string AssetId { get; set; } = string.Empty;
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("coinName")]
        public string AssetName { get; set; } = string.Empty;
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Address tag
        /// </summary>
        [JsonPropertyName("addressExt")]
        public string Tag { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Arrive quantity
        /// </summary>
        [JsonPropertyName("arriveQuantity")]
        public decimal ArriveQuantity { get; set; }
        /// <summary>
        /// Withdrawal status code
        /// </summary>
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; } = string.Empty;
        /// <summary>
        /// Withdrawal status
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }
        /// <summary>
        /// Transaction id 
        /// </summary>
        [JsonPropertyName("txId ")]
        public string? TransactionId { get; set; }
        /// <summary>
        /// Transaction id url 
        /// </summary>
        [JsonPropertyName("txIdUrl ")]
        public string? TransactionIdUrl { get; set; }
        /// <summary>
        /// Wallet handle time
        /// </summary>
        [JsonPropertyName("walletHandleTime")]
        public DateTime? WalletHandleTime { get; set; }
        /// <summary>
        /// Fee asset id 
        /// </summary>
        [JsonPropertyName("feeCoinId ")]
        public string FeeAssetId { get; set; } = string.Empty;
        /// <summary>
        /// Fee asset name 
        /// </summary>
        [JsonPropertyName("feeCoinName ")]
        public string FeeAssetName { get; set; } = string.Empty;
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Required confirmation times 
        /// </summary>
        [JsonPropertyName("requiredConfirmTimes ")]
        public int? RequiredConfirmTimes { get; set; }
        /// <summary>
        /// Confirm times 
        /// </summary>
        [JsonPropertyName("confirmTimes ")]
        public int? ConfirmTimes { get; set; }
        /// <summary>
        /// Kernel id
        /// </summary>
        [JsonPropertyName("kernelId")]
        public string? KernelId { get; set; }
        /// <summary>
        /// Is internal transfer
        /// </summary>
        [JsonPropertyName("isInternalTransfer")]
        public bool IsInternalTransfer { get; set; }
    }


}
