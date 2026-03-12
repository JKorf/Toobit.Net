using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Time in force
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TimeInForce>))]
    public enum TimeInForce
    {
        /// <summary>
        /// ["<c>GTC</c>"] Good till canceled
        /// </summary>
        [Map("GTC")]
        GoodTillCanceled,
        /// <summary>
        /// ["<c>IOC</c>"] Immediate or cancel
        /// </summary>
        [Map("IOC")]
        ImmediateOrCancel,
        /// <summary>
        /// ["<c>FOK</c>"] Fill or kill
        /// </summary>
        [Map("FOK")]
        FillOrKill,
        /// <summary>
        /// ["<c>LIMIT_MAKER</c>"] Limit maker
        /// </summary>
        [Map("LIMIT_MAKER")]
        LimitMaker
    }
}
