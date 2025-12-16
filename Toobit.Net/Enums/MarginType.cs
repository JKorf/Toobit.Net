using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

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
