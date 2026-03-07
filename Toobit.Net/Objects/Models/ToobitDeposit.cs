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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>coin</c>"] Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
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
        /// ["<c>addressTag</c>"] Address tag
        /// </summary>
        [JsonPropertyName("addressTag")]
        public string? AddressTag { get; set; }
        /// <summary>
        /// ["<c>fromAddress</c>"] From address
        /// </summary>
        [JsonPropertyName("fromAddress")]
        public string FromAddress { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fromAddressTag</c>"] From address tag
        /// </summary>
        [JsonPropertyName("fromAddressTag")]
        public string? FromAddressTag { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>quantity</c>"] Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public DepositStatus Status { get; set; }
        /// <summary>
        /// ["<c>statusCode</c>"] Status code
        /// </summary>
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>requiredConfirmTimes</c>"] Required confirm times
        /// </summary>
        [JsonPropertyName("requiredConfirmTimes")]
        public int? RequiredConfirmTimes { get; set; }
        /// <summary>
        /// ["<c>confirmTimes</c>"] Confirm times
        /// </summary>
        [JsonPropertyName("confirmTimes")]
        public int? ConfirmTimes { get; set; }
        /// <summary>
        /// ["<c>txId</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("txId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>txIdUrl</c>"] Transaction id url
        /// </summary>
        [JsonPropertyName("txIdUrl")]
        public string? TransactionIdUrl { get; set; }
    }


}
