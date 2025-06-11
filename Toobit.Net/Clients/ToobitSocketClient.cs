using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Toobit.Net.Clients.SpotApi;
using Toobit.Net.Clients.UsdtFuturesApi;
using Toobit.Net.Interfaces.Clients;
using Toobit.Net.Interfaces.Clients.SpotApi;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;
using Toobit.Net.Objects.Options;

namespace Toobit.Net.Clients
{
    /// <inheritdoc cref="IToobitSocketClient" />
    public class ToobitSocketClient : BaseSocketClient, IToobitSocketClient
    {
        #region fields
        #endregion

        #region Api clients

        
         /// <inheritdoc />
        public IToobitSocketClientUsdtFuturesApi UsdtFuturesApi { get; }

         /// <inheritdoc />
        public IToobitSocketClientSpotApi SpotApi { get; }


        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of ToobitSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public ToobitSocketClient(Action<ToobitSocketOptions>? optionsDelegate = null)
            : this(Options.Create(ApplyOptionsDelegate(optionsDelegate)), null)
        {
        }

        /// <summary>
        /// Create a new instance of ToobitSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="options">Option configuration</param>
        public ToobitSocketClient(IOptions<ToobitSocketOptions> options, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "Toobit")
        {
            Initialize(options.Value);
                        
            UsdtFuturesApi = AddApiClient(new ToobitSocketClientUsdtFuturesApi(_logger, options.Value));
            SpotApi = AddApiClient(new ToobitSocketClientSpotApi(_logger, options.Value));
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
        public static void SetDefaultOptions(Action<ToobitSocketOptions> optionsDelegate)
        {
            ToobitSocketOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            
            UsdtFuturesApi.SetApiCredentials(credentials);

            SpotApi.SetApiCredentials(credentials);

        }
    }
}
