using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesOrderType>))]
    public enum FuturesOrderType
    {
        /// <summary>
        /// ["<c>LIMIT</c>"] Limit order
        /// </summary>
        [Map("LIMIT")]
        Limit,
        /// <summary>
        /// ["<c>Market</c>"] Limit order
        /// </summary>
        [Map("Market")]
        Market,
        /// <summary>
        /// ["<c>STOP</c>"] Stop order
        /// </summary>
        [Map("STOP")]
        Stop
    }
}
