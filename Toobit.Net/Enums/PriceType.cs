using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PriceType>))]
    public enum PriceType
    {
        /// <summary>
        /// ["<c>INPUT</c>"] The system will match the order with the price you entered.
        /// </summary>
        [Map("INPUT")]
        Input,
        /// <summary>
        /// ["<c>OPPONENT</c>"] The order will be matched at the best price of the counterparty.
        /// </summary>
        [Map("OPPONENT")]
        Opponent,
        /// <summary>
        /// ["<c>QUEUE</c>"] Orders will be matched at the best price in the same direction.
        /// </summary>
        [Map("QUEUE")]
        Queue,
        /// <summary>
        /// ["<c>OVER</c>"] The order will be matched at the best price of the counterparty + over-price (floating).
        /// </summary>
        [Map("OVER")]
        Over,
        /// <summary>
        /// ["<c>MARKET</c>"] Orders will be matched at the latest transaction price * (1 ± 5%).
        /// </summary>
        [Map("MARKET")]
        Market
    }
}
