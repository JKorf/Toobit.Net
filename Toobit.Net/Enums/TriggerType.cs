using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Trigger type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerType>))]
    public enum TriggerType
    {
        /// <summary>
        /// ["<c>MARK_PRICE</c>"] Mark price
        /// </summary>
        [Map("MARK_PRICE")]
        MarkPrice,
        /// <summary>
        /// ["<c>CONTRACT_PRICE</c>"] Contract price
        /// </summary>
        [Map("CONTRACT_PRICE")]
        ContractPrice
    }
}
