using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesNewOrderType>))]
    public enum FuturesNewOrderType
    {
        /// <summary>
        /// ["<c>LIMIT</c>"] Limit order
        /// </summary>
        [Map("LIMIT")]
        Limit,
        /// <summary>
        /// ["<c>STOP</c>"] Stop order
        /// </summary>
        [Map("STOP")]
        Stop
    }
}
