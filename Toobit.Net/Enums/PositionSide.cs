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
        /// ["<c>LONG</c>"] Long
        /// </summary>
        [Map("LONG")]
        Long,
        /// <summary>
        /// ["<c>SHORT</c>"] Short
        /// </summary>
        [Map("SHORT")]
        Short
    }
}
