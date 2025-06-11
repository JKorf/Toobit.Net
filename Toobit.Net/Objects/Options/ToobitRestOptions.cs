using CryptoExchange.Net.Objects.Options;
using System;

namespace Toobit.Net.Objects.Options
{
    /// <summary>
    /// Options for the ToobitRestClient
    /// </summary>
    public class ToobitRestOptions : RestExchangeOptions<ToobitEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static ToobitRestOptions Default { get; set; } = new ToobitRestOptions()
        {
            Environment = ToobitEnvironment.Live,
            AutoTimestamp = true
        };

        /// <summary>
        /// ctor
        /// </summary>
        public ToobitRestOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// The default receive window for requests
        /// </summary>
        public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// UsdtFutures API options
        /// </summary>
        public RestApiOptions UsdtFuturesOptions { get; private set; } = new RestApiOptions();

         /// <summary>
        /// Spot API options
        /// </summary>
        public RestApiOptions SpotOptions { get; private set; } = new RestApiOptions();

        internal ToobitRestOptions Set(ToobitRestOptions targetOptions)
        {
            targetOptions = base.Set<ToobitRestOptions>(targetOptions);            
            targetOptions.UsdtFuturesOptions = UsdtFuturesOptions.Set(targetOptions.UsdtFuturesOptions);
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            return targetOptions;
        }
    }
}
