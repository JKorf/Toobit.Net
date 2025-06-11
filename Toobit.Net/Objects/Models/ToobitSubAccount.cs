using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Toobit.Net.Enums;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Sub account
    /// </summary>
    public record ToobitSubAccount
    {
        /// <summary>
        /// Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// Nick name
        /// </summary>
        [JsonPropertyName("nickName")]
        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public AccountType AccountType { get; set; }
        /// <summary>
        /// Account index
        /// </summary>
        [JsonPropertyName("accountIndex")]
        public int AccountIndex { get; set; }
    }


}
