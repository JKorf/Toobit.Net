using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Deposit address
    /// </summary>
    public record ToobitDepositAddress
    {
        /// <summary>
        /// ["<c>canDeposit</c>"] Can deposit
        /// </summary>
        [JsonPropertyName("canDeposit")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// ["<c>needAddressTag</c>"] Address needs tag
        /// </summary>
        [JsonPropertyName("needAddressTag")]
        public bool NeedAddressTag { get; set; }
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
        /// ["<c>minQuantity</c>"] Min quantity
        /// </summary>
        [JsonPropertyName("minQuantity")]
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// ["<c>requiredConfirmTimes</c>"] Required confirmation times 
        /// </summary>
        [JsonPropertyName("requiredConfirmTimes")]
        public int RequiredConfirmationTimes { get; set; }
        /// <summary>
        /// ["<c>canWithdrawConfirmTimes</c>"] Number of confirmations before withdrawal is allowed
        /// </summary>
        [JsonPropertyName("canWithdrawConfirmTimes")]
        public decimal CanWithdrawConfirmNumber { get; set; }
        /// <summary>
        /// ["<c>coinType</c>"] Asset type
        /// </summary>
        [JsonPropertyName("coinType")]
        public string AssetType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>chainType</c>"] Network type
        /// </summary>
        [JsonPropertyName("chainType")]
        public string NetworkType { get; set; } = string.Empty;
    }


}
