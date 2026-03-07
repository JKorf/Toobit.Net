using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// User balances
    /// </summary>
    public record ToobitUserBalance
    {
        /// <summary>
        /// ["<c>userId</c>"] User Id
        /// </summary>
        [JsonPropertyName("userId")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>balances</c>"] Balances
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
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>assetId</c>"] Asset id
        /// </summary>
        [JsonPropertyName("assetId")]
        public string AssetId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>assetName</c>"] Asset name
        /// </summary>
        [JsonPropertyName("assetName")]
        public string AssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>total</c>"] Total balance
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
        /// <summary>
        /// ["<c>free</c>"] Free balance
        /// </summary>
        [JsonPropertyName("free")]
        public decimal Free { get; set; }
        /// <summary>
        /// ["<c>locked</c>"] Locked balance
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
    }
}
