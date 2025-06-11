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
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesUpdateOrderType>))]
    public enum FuturesUpdateOrderType
    {
        /// <summary>
        /// Limit order
        /// </summary>
        [Map("LIMIT")]
        Limit,
        /// <summary>
        /// Market
        /// </summary>
        [Map("MARKET", "MARKET_OF_BASE")]
        Market,
        /// <summary>
        /// Limit maker
        /// </summary>
        [Map("LIMIT_MAKER")]
        LimitMaker,
        /// <summary>
        /// Stop limit order
        /// </summary>
        [Map("STOP_LIMIT")]
        StopLimit
    }
}
