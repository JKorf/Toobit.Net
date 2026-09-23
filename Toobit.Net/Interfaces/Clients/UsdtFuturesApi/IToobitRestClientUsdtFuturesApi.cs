using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace Toobit.Net.Interfaces.Clients.UsdtFuturesApi
{
    /// <summary>
    /// Toobit UsdtFutures API endpoints
    /// </summary>
    public interface IToobitRestClientUsdtFuturesApi : IRestApiClient<ToobitCredentials>, IDisposable
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
        /// [V1] Get the shared rest requests client. For new implementations prefer <see cref="SharedApi"/>
        /// </summary>
        public IToobitRestClientUsdtFuturesApiShared SharedClient { get; }
        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IToobitRestClientUsdtFuturesSharedApi SharedApi { get; }
    }
}
