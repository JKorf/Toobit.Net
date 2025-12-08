using System;
using System.Text.Json.Serialization;
using Toobit.Net.Enums;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Futures transaction
    /// </summary>
    public record ToobitFuturesTransaction
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }
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
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Symbol id
        /// </summary>
        [JsonPropertyName("symbolId")]
        public string SymbolId { get; set; } = string.Empty;
        /// <summary>
        /// Flow type
        /// </summary>
        [JsonPropertyName("flowTypeValue")]
        public FlowType FlowType { get; set; }
        /// <summary>
        /// Flow type description
        /// </summary>
        [JsonPropertyName("flowType")]
        public string FlowTypeDescription { get; set; } = string.Empty;
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
