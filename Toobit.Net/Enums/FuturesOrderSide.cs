using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesOrderSide>))]
    public enum FuturesOrderSide
    {
        /// <summary>
        /// ["<c>BUY_OPEN</c>"] Buy open long
        /// </summary>
        [Map("BUY_OPEN")]
        BuyOpen,
        /// <summary>
        /// ["<c>BUY_CLOSE</c>"] Buy close short
        /// </summary>
        [Map("BUY_CLOSE")]
        BuyClose,
        /// <summary>
        /// ["<c>SELL_OPEN</c>"] Sell open short
        /// </summary>
        [Map("SELL_OPEN")]
        SellOpen,
        /// <summary>
        /// ["<c>SELL_CLOSE</c>"] Sell close long
        /// </summary>
        [Map("SELL_CLOSE")]
        SellClose
    }
}
