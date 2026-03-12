using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderType>))]
    public enum OrderType
    {
        /// <summary>
        /// ["<c>LIMIT</c>"] Limit order
        /// </summary>
        [Map("LIMIT")]
        Limit,
        /// <summary>
        /// ["<c>MARKET</c>"] Market order
        /// </summary>
        [Map("MARKET", "MARKET_OF_QUOTE", "MARKET_OF_BASE")]
        Market,
        /// <summary>
        /// ["<c>LIMIT_MAKER</c>"] Limit maker
        /// </summary>
        [Map("LIMIT_MAKER")]
        LimitMaker
    }
}
