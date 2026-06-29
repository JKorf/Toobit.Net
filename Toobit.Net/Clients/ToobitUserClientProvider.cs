using Toobit.Net.Interfaces.Clients;
using Toobit.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using CryptoExchange.Net.Clients;

namespace Toobit.Net.Clients
{
    /// <inheritdoc />
    public class ToobitUserClientProvider : UserClientProvider<
        IToobitRestClient,
        IToobitSocketClient,
        ToobitRestOptions,
        ToobitSocketOptions,
        ToobitCredentials,
        ToobitEnvironment
        >, IToobitUserClientProvider
    {
        /// <inheritdoc />
        public override string ExchangeName => ToobitExchange.ExchangeName;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public ToobitUserClientProvider(Action<ToobitOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate).Rest), Options.Create(ApplyOptionsDelegate(optionsDelegate).Socket))
        {
        }
        
        /// <summary>
        /// ctor
        /// </summary>
        public ToobitUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<ToobitRestOptions> restOptions,
            IOptions<ToobitSocketOptions> socketOptions)
            : base(httpClient, loggerFactory, restOptions, socketOptions)
        {
        }

        /// <inheritdoc />
        protected override IToobitRestClient ConstructRestClient(HttpClient client, ILoggerFactory? loggerFactory, IOptions<ToobitRestOptions> options)
            => new ToobitRestClient(client, loggerFactory, options);
        /// <inheritdoc />
        protected override IToobitSocketClient ConstructSocketClient(ILoggerFactory? loggerFactory, IOptions<ToobitSocketOptions> options) 
            => new ToobitSocketClient(options, loggerFactory);
    }
}
