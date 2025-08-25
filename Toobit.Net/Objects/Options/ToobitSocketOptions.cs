using CryptoExchange.Net.Objects.Options;
using System;

namespace Toobit.Net.Objects.Options
{
    /// <summary>
    /// Options for the ToobitSocketClient
    /// </summary>
    public class ToobitSocketOptions : SocketExchangeOptions<ToobitEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static ToobitSocketOptions Default { get; set; } = new ToobitSocketOptions()
        {
            Environment = ToobitEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };


        /// <summary>
        /// ctor
        /// </summary>
        public ToobitSocketOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// The server only replies with a message when there is an error in the subscription, not when it's successful. This timeout determines how
        /// long to wait at max for an error message before the subscription is assumed successful. Note that when data is received on the subscription
        /// before this timeout it is also deemed successful
        /// </summary>
        public TimeSpan SubscribeMaxWaitForError { get; set; } = TimeSpan.FromSeconds(1);

         /// <summary>
        /// UsdtFutures API options
        /// </summary>
        public SocketApiOptions UsdtFuturesOptions { get; private set; } = new SocketApiOptions();

         /// <summary>
        /// Spot API options
        /// </summary>
        public SocketApiOptions SpotOptions { get; private set; } = new SocketApiOptions();


        internal ToobitSocketOptions Set(ToobitSocketOptions targetOptions)
        {
            targetOptions = base.Set<ToobitSocketOptions>(targetOptions);
            targetOptions.SubscribeMaxWaitForError = SubscribeMaxWaitForError;
            targetOptions.UsdtFuturesOptions = UsdtFuturesOptions.Set(targetOptions.UsdtFuturesOptions);
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            return targetOptions;
        }
    }
}
