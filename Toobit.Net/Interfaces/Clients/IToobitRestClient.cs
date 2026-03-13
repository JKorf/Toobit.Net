using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using Toobit.Net.Interfaces.Clients.SpotApi;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;

namespace Toobit.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Toobit Rest API. 
    /// </summary>
    public interface IToobitRestClient : IRestClient<ToobitCredentials>
    {
        /// <summary>
        /// UsdtFutures API endpoints
        /// </summary>
        /// <see cref="IToobitRestClientUsdtFuturesApi"/>
        public IToobitRestClientUsdtFuturesApi UsdtFuturesApi { get; }

        /// <summary>
        /// Spot API endpoints
        /// </summary>
        /// <see cref="IToobitRestClientSpotApi"/>
        public IToobitRestClientSpotApi SpotApi { get; }
    }
}
