using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Ticker stats
    /// </summary>
    public enum TickerInterval
    {
        /// <summary>
        /// 24 hours
        /// </summary>
        [Map("24h")]
        H24,
        /// <summary>
        /// One day, reset at 00:00 UTC
        /// </summary>
        [Map("1d")]
        D1,
        /// <summary>
        /// One day, reset at 00:00 UTC+8
        /// </summary>
        [Map("1d+8")]
        D1Utc8
    }
}
