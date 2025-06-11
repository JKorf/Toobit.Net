﻿using CryptoExchange.Net.Attributes;
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
    [JsonConverter(typeof(EnumConverter<OrderType>))]
    public enum OrderType
    {
        /// <summary>
        /// Limit order
        /// </summary>
        [Map("LIMIT")]
        Limit,
        /// <summary>
        /// Market order
        /// </summary>
        [Map("MARKET", "MARKET_OF_QUOTE", "MARKET_OF_BASE")]
        Market,
        /// <summary>
        /// Limit maker
        /// </summary>
        [Map("LIMIT_MAKER")]
        LimitMaker
    }
}
