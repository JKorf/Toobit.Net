using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// RWA type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<RwaType>))]
    public enum RwaType
    {
        /// <summary>
        /// ["<c>STOCK</c>"] Stock
        /// </summary>
        [Map("STOCK")]
        Stock
    }
}
