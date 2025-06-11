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
    /// Time in force
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TimeInForce>))]
    public enum TimeInForce
    {
        /// <summary>
        /// Good till canceled
        /// </summary>
        [Map("GTC")]
        GoodTillCanceled,
        /// <summary>
        /// Immediate or cancel
        /// </summary>
        [Map("IOC")]
        ImmediateOrCancel,
        /// <summary>
        /// Fill or kill
        /// </summary>
        [Map("FOK")]
        FillOrKill,
        /// <summary>
        /// Limit maker
        /// </summary>
        [Map("LIMIT_MAKER")]
        LimitMaker
    }
}
