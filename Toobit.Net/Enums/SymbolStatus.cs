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
        /// ["<c>TRADING</c>"] Trading
        /// </summary>
        [Map("TRADING")]
        Trading,
        /// <summary>
        /// ["<c>API_TRADE_FORBIDDEN</c>"] Api trading forbidden
        /// </summary>
        [Map("API_TRADE_FORBIDDEN")]
        ApiTradeForbidden,
        /// <summary>
        /// ["<c>Offline</c>"] Offline
        /// </summary>
        [Map("Offline")]
        Offline,
        /// <summary>
        /// ["<c>ONLINE</c>"] Online
        /// </summary>
        [Map("ONLINE")]
        Online,
    }
}
