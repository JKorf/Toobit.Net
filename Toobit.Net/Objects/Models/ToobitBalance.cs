using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// User balances
    /// </summary>
    public record ToobitUserBalance
    {
        /// <summary>
        /// User Id
        /// </summary>
        [JsonPropertyName("userId")]
        public long UserId { get; set; }
        /// <summary>
        /// Balances
        /// </summary>
        [JsonPropertyName("balances")]
        public ToobitBalance[] Balances { get; set; } = [];
    }

    /// <summary>
    /// Balance info
    /// </summary>
    public record ToobitBalance
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Asset id
        /// </summary>
        [JsonPropertyName("assetId")]
        public string AssetId { get; set; } = string.Empty;
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("assetName")]
        public string AssetName { get; set; } = string.Empty;
        /// <summary>
        /// Total balance
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
        /// <summary>
        /// Free balance
        /// </summary>
        [JsonPropertyName("free")]
        public decimal Free { get; set; }
        /// <summary>
        /// Locked balance
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
    }
}
