using System;
using System.Text.Json.Serialization;
using Toobit.Net.Enums;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// 
    /// </summary>
    public record ToobitDeposit
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
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
        [JsonPropertyName("addressTag")]
        public string? AddressTag { get; set; }
        /// <summary>
        /// From address
        /// </summary>
        [JsonPropertyName("fromAddress")]
        public string FromAddress { get; set; } = string.Empty;
        /// <summary>
        /// From address tag
        /// </summary>
        [JsonPropertyName("fromAddressTag")]
        public string? FromAddressTag { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public DepositStatus Status { get; set; }
        /// <summary>
        /// Status code
        /// </summary>
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; } = string.Empty;
        /// <summary>
        /// Required confirm times
        /// </summary>
        [JsonPropertyName("requiredConfirmTimes")]
        public int? RequiredConfirmTimes { get; set; }
        /// <summary>
        /// Confirm times
        /// </summary>
        [JsonPropertyName("confirmTimes")]
        public int? ConfirmTimes { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("txId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Transaction id url
        /// </summary>
        [JsonPropertyName("txIdUrl")]
        public string? TransactionIdUrl { get; set; }
    }


}
