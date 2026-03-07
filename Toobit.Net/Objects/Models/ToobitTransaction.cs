using System;
using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Transaction info
    /// </summary>
    public record ToobitTransaction
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>accountId</c>"] Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>coin</c>"] Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>coinId</c>"] Asset id
        /// </summary>
        [JsonPropertyName("coinId")]
        public string AssetId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>coinName</c>"] Asset name
        /// </summary>
        [JsonPropertyName("coinName")]
        public string AssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>flowTypeValue</c>"] Flow type value
        /// </summary>
        [JsonPropertyName("flowTypeValue")]
        public int FlowTypeValue { get; set; }
        /// <summary>
        /// ["<c>flowType</c>"] Flow type
        /// </summary>
        [JsonPropertyName("flowType")]
        public string FlowType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>flowName</c>"] Flow name
        /// </summary>
        [JsonPropertyName("flowName")]
        public string FlowName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>change</c>"] Change
        /// </summary>
        [JsonPropertyName("change")]
        public decimal Change { get; set; }
        /// <summary>
        /// ["<c>total</c>"] Total
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
        /// <summary>
        /// ["<c>created</c>"] Create time
        /// </summary>
        [JsonPropertyName("created")]
        public DateTime CreateTime { get; set; }
    }


}
