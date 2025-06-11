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
    /// Trigger type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerType>))]
    public enum TriggerType
    {
        /// <summary>
        /// Mark price
        /// </summary>
        [Map("MARK_PRICE")]
        MarkPrice,
        /// <summary>
        /// Contract price
        /// </summary>
        [Map("CONTRACT_PRICE")]
        ContractPrice
    }
}
