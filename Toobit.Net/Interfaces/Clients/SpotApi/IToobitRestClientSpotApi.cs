using CryptoExchange.Net.Interfaces;
using System;

namespace Toobit.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Toobit Spot API endpoints
    /// </summary>
    public interface IToobitRestClientSpotApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IToobitRestClientSpotApiAccount" />
        public IToobitRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IToobitRestClientSpotApiExchangeData" />
        public IToobitRestClientSpotApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IToobitRestClientSpotApiTrading" />
        public IToobitRestClientSpotApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IToobitRestClientSpotApiShared SharedClient { get; }
    }
}
