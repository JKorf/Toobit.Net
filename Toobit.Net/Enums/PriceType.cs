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
    /// Price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PriceType>))]
    public enum PriceType
    {
        /// <summary>
        /// The system will match the order with the price you entered.
        /// </summary>
        [Map("INPUT")]
        Input,
        /// <summary>
        /// The order will be matched at the best price of the counterparty.
        /// </summary>
        [Map("OPPONENT")]
        Opponent,
        /// <summary>
        /// Orders will be matched at the best price in the same direction.
        /// </summary>
        [Map("QUEUE")]
        Queue,
        /// <summary>
        /// The order will be matched at the best price of the counterparty + over-price (floating).
        /// </summary>
        [Map("OVER")]
        Over,
        /// <summary>
        /// Orders will be matched at the latest transaction price * (1 ± 5%).
        /// </summary>
        [Map("MARKET")]
        Market
    }
}
