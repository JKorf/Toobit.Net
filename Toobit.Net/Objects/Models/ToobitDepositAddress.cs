using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// 
    /// </summary>
    public record ToobitDepositAddress
    {
        /// <summary>
        /// Can deposit
        /// </summary>
        [JsonPropertyName("canDeposit")]
        public bool CanDeposit { get; set; }
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
        /// Min quantity
        /// </summary>
        [JsonPropertyName("minQuantity")]
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// Required confirmation times 
        /// </summary>
        [JsonPropertyName("requiredConfirmTimes ")]
        public int RequiredConfirmationTimes { get; set; }
        /// <summary>
        /// Number of confirmations before withdrawal is allowed
        /// </summary>
        [JsonPropertyName("canWithdrawConfirmNum ")]
        public decimal CanWithdrawConfirmNumber { get; set; }
        /// <summary>
        /// Asset type
        /// </summary>
        [JsonPropertyName("coinType")]
        public string AssetType { get; set; } = string.Empty;
    }


}
