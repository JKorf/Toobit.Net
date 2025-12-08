using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace Toobit.Net.Interfaces.Clients.UsdtFuturesApi
{
    /// <summary>
    /// Toobit UsdtFutures API endpoints
    /// </summary>
    public interface IToobitRestClientUsdtFuturesApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IToobitRestClientUsdtFuturesApiAccount" />
        public IToobitRestClientUsdtFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IToobitRestClientUsdtFuturesApiExchangeData" />
        public IToobitRestClientUsdtFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IToobitRestClientUsdtFuturesApiTrading" />
        public IToobitRestClientUsdtFuturesApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IToobitRestClientUsdtFuturesApiShared SharedClient { get; }
    }
}
