using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Options;
using Toobit.Net.Interfaces.Clients.SpotApi;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;
using Toobit.Net.Objects.Options;

namespace Toobit.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Toobit Rest API. 
    /// </summary>
    public interface IToobitRestClient : IRestClient
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

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);

        /// <summary>
        /// Update specific options
        /// </summary>
        /// <param name="options">Options to update. Only specific options are changeable after the client has been created</param>
        void SetOptions(UpdateOptions options);
    }
}
