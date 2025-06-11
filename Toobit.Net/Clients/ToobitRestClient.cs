using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using CryptoExchange.Net.Authentication;
using Toobit.Net.Interfaces.Clients;
using Toobit.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Options;
using CryptoExchange.Net.Objects.Options;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;
using Toobit.Net.Interfaces.Clients.SpotApi;
using Toobit.Net.Clients.UsdtFuturesApi;
using Toobit.Net.Clients.SpotApi;

namespace Toobit.Net.Clients
{
    /// <inheritdoc cref="IToobitRestClient" />
    public class ToobitRestClient : BaseRestClient, IToobitRestClient
    {
        #region Api clients
                
         /// <inheritdoc />
        public IToobitRestClientUsdtFuturesApi UsdtFuturesApi { get; }

         /// <inheritdoc />
        public IToobitRestClientSpotApi SpotApi { get; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of the ToobitRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public ToobitRestClient(Action<ToobitRestOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate)))
        {
        }

        /// <summary>
        /// Create a new instance of the ToobitRestClient using provided options
        /// </summary>
        /// <param name="options">Option configuration</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public ToobitRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<ToobitRestOptions> options) : base(loggerFactory, "Toobit")
        {
            Initialize(options.Value);
                        
            UsdtFuturesApi = AddApiClient(new ToobitRestClientUsdtFuturesApi(_logger, httpClient, options.Value));
            SpotApi = AddApiClient(new ToobitRestClientSpotApi(_logger, httpClient, options.Value));
        }

        #endregion

        /// <inheritdoc />
        public void SetOptions(UpdateOptions options)
        {
            UsdtFuturesApi.SetOptions(options);
            SpotApi.SetOptions(options);
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<ToobitRestOptions> optionsDelegate)
        {
            ToobitRestOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {            
            UsdtFuturesApi.SetApiCredentials(credentials);
            SpotApi.SetApiCredentials(credentials);
        }
    }
}
