using CryptoExchange.Net.Objects.Options;

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
            
            targetOptions.UsdtFuturesOptions = UsdtFuturesOptions.Set(targetOptions.UsdtFuturesOptions);

            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);

            return targetOptions;
        }
    }
}
