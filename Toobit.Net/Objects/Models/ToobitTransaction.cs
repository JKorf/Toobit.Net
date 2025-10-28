using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Transaction info
    /// </summary>
    public record ToobitTransaction
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Asset id
        /// </summary>
        [JsonPropertyName("coinId")]
        public string AssetId { get; set; } = string.Empty;
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("coinName")]
        public string AssetName { get; set; } = string.Empty;
        /// <summary>
        /// Flow type value
        /// </summary>
        [JsonPropertyName("flowTypeValue")]
        public int FlowTypeValue { get; set; }
        /// <summary>
        /// Flow type
        /// </summary>
        [JsonPropertyName("flowType")]
        public string FlowType { get; set; } = string.Empty;
        /// <summary>
        /// Flow name
        /// </summary>
        [JsonPropertyName("flowName")]
        public string FlowName { get; set; } = string.Empty;
        /// <summary>
        /// Change
        /// </summary>
        [JsonPropertyName("change")]
        public decimal Change { get; set; }
        /// <summary>
        /// Total
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("created")]
        public DateTime CreateTime { get; set; }
    }


}
