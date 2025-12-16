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
        /// Success
        /// </summary>
        [Map("2")]
        Success,
        /// <summary>
        /// Rejected
        /// </summary>
        [Map("11")]
        Rejected,
        /// <summary>
        /// Under audit
        /// </summary>
        [Map("12")]
        Audit
    }
}
