using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Position side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionSide>))]
    public enum PositionSide
    {
        /// <summary>
        /// Long
        /// </summary>
        [Map("LONG")]
        Long,
        /// <summary>
        /// Short
        /// </summary>
        [Map("SHORT")]
        Short
    }
}
