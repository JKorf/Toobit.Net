using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderSide>))]
    public enum OrderSide
    {
        /// <summary>
        /// ["<c>BUY</c>"] Buy
        /// </summary>
        [Map("BUY")]
        Buy,
        /// <summary>
        /// ["<c>SELL</c>"] Sell
        /// </summary>
        [Map("SELL")]
        Sell
    }
}
