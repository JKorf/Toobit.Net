using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderStatus>))]
    public enum OrderStatus
    {
        /// <summary>
        /// Order is new
        /// </summary>
        [Map("NEW", "PENDING_NEW", "ORDER_NEW")]
        New,
        /// <summary>
        /// Order is partly filled, still has quantity left to fill
        /// </summary>
        [Map("PARTIALLY_FILLED")]
        PartiallyFilled,
        /// <summary>
        /// The order has been filled and completed
        /// </summary>
        [Map("FILLED")]
        Filled,
        /// <summary>
        /// The order has been canceled
        /// </summary>
        [Map("CANCELED", "ORDER_CANCELED")]
        Canceled,
        /// <summary>
        /// Pending cancel
        /// </summary>
        [Map("PENDING_CANCEL")]
        PendingCancel,
        /// <summary>
        /// Rejected
        /// </summary>
        [Map("REJECTED")]
        Rejected,
        /// <summary>
        /// The order was canceled after being partially filled
        /// </summary>
        [Map("PARTIALLY_CANCELED")]
        PartiallyCanceled
    }
}
