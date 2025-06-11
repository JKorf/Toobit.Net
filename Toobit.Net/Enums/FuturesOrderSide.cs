using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesOrderSide>))]
    public enum FuturesOrderSide
    {
        /// <summary>
        /// Buy open long
        /// </summary>
        [Map("BUY_OPEN")]
        BuyOpen,
        /// <summary>
        /// Buy close short
        /// </summary>
        [Map("BUY_CLOSE")]
        BuyClose,
        /// <summary>
        /// Sell open short
        /// </summary>
        [Map("SELL_OPEN")]
        SellOpen,
        /// <summary>
        /// Sell close long
        /// </summary>
        [Map("SELL_CLOSE")]
        SellClose
    }
}
