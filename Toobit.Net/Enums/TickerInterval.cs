using CryptoExchange.Net.Attributes;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Ticker stats
    /// </summary>
    public enum TickerInterval
    {
        /// <summary>
        /// ["<c>24h</c>"] 24 hours
        /// </summary>
        [Map("24h")]
        H24,
        /// <summary>
        /// ["<c>1d</c>"] One day, reset at 00:00 UTC
        /// </summary>
        [Map("1d")]
        D1,
        /// <summary>
        /// ["<c>1d+8</c>"] One day, reset at 00:00 UTC+8
        /// </summary>
        [Map("1d+8")]
        D1Utc8
    }
}
