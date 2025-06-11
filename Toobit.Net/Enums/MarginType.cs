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
    /// Type of margin
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginType>))]
    public enum MarginType
    {
        /// <summary>
        /// Isolated
        /// </summary>
        [Map("ISOLATED")]
        Isolated,
        /// <summary>
        /// Cross
        /// </summary>
        [Map("CROSS")]
        Cross
    }
}
