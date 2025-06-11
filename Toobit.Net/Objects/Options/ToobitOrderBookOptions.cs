using CryptoExchange.Net.Objects.Options;
using System;

namespace Toobit.Net.Objects.Options
{
    /// <summary>
    /// Options for the Toobit SymbolOrderBook
    /// </summary>
    public class ToobitOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        public static ToobitOrderBookOptions Default { get; set; } = new ToobitOrderBookOptions();

        /// <summary>
        /// After how much time we should consider the connection dropped if no data is received for this time after the initial subscriptions
        /// </summary>
        public TimeSpan? InitialDataTimeout { get; set; }

        internal ToobitOrderBookOptions Copy()
        {
            var result = Copy<ToobitOrderBookOptions>();
            result.InitialDataTimeout = InitialDataTimeout;
            return result;
        }
    }
}
