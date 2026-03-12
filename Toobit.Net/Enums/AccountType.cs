using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountType>))]
    public enum AccountType
    {
        /// <summary>
        /// ["<c>MAIN</c>"] Spot account
        /// </summary>
        [Map("MAIN", "1")]
        Spot,
        /// <summary>
        /// ["<c>FUTURES</c>"] Futures account
        /// </summary>
        [Map("FUTURES", "3")]
        Futures
    }
}
