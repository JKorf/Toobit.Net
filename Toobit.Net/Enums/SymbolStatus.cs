using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Symbol trading status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SymbolStatus>))]
    public enum SymbolStatus
    {
        /// <summary>
        /// Trading
        /// </summary>
        [Map("TRADING")]
        Trading,
        /// <summary>
        /// Api trading forbidden
        /// </summary>
        [Map("API_TRADE_FORBIDDEN")]
        ApiTradeForbidden
    }
}
