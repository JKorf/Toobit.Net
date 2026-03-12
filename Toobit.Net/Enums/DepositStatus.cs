using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Deposit status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<DepositStatus>))]
    public enum DepositStatus
    {
        /// <summary>
        /// ["<c>2</c>"] Success
        /// </summary>
        [Map("2")]
        Success,
        /// <summary>
        /// ["<c>11</c>"] Rejected
        /// </summary>
        [Map("11")]
        Rejected,
        /// <summary>
        /// ["<c>12</c>"] Under audit
        /// </summary>
        [Map("12")]
        Audit
    }
}
