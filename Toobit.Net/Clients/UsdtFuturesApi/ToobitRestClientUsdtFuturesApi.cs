using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Toobit.Net.Clients.MessageHandlers;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;
using Toobit.Net.Objects.Options;

namespace Toobit.Net.Clients.UsdtFuturesApi
{
    /// <inheritdoc cref="IToobitRestClientUsdtFuturesApi" />
    internal partial class ToobitRestClientUsdtFuturesApi : RestApiClient<ToobitEnvironment, ToobitAuthenticationProvider, ToobitCredentials>, IToobitRestClientUsdtFuturesApi
    {
        #region fields 
        protected override ErrorMapping ErrorMapping => ToobitErrors.Errors;
        protected override IRestMessageHandler MessageHandler { get; } = new ToobitRestMessageHandler(ToobitErrors.Errors);

        #endregion

        #region Api clients
        /// <inheritdoc />
        public IToobitRestClientUsdtFuturesApiAccount Account { get; }
        /// <inheritdoc />
        public IToobitRestClientUsdtFuturesApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IToobitRestClientUsdtFuturesApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "Toobit";
        #endregion

        #region constructor/destructor
        internal ToobitRestClientUsdtFuturesApi(ILoggerFactory? loggerFactory, HttpClient? httpClient, ToobitRestOptions options)
            : base(loggerFactory, ToobitExchange.Metadata.Id, httpClient, options.Environment.RestClientAddress, options, options.UsdtFuturesOptions)
        {
            RequestBodyFormat = RequestBodyFormat.FormData;
            RequestBodyEmptyContent = "";

            Account = new ToobitRestClientUsdtFuturesApiAccount(this);
            ExchangeData = new ToobitRestClientUsdtFuturesApiExchangeData(_logger, this);
            Trading = new ToobitRestClientUsdtFuturesApiTrading(_logger, this);
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(ToobitExchange._serializerContext));


        /// <inheritdoc />
        protected override ToobitAuthenticationProvider CreateAuthenticationProvider(ToobitCredentials credentials)
            => new ToobitAuthenticationProvider(credentials);

        internal async Task<HttpResult> SendAsync(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<Unit>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            return result;
        }

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            var result = await base.SendAsync<T>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);

            // Optional response checking

            return result;
        }

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null) 
            => ToobitExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);

        /// <inheritdoc />
        public IToobitRestClientUsdtFuturesApiShared SharedClient => this;

    }
}
