using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Toobit.Net.Interfaces.Clients.SpotApi;
using Toobit.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using Toobit.Net.Clients.MessageHandlers;
using System.Net.Http.Headers;

namespace Toobit.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IToobitRestClientSpotApi" />
    internal partial class ToobitRestClientSpotApi : RestApiClient<ToobitEnvironment, ToobitAuthenticationProvider, ToobitCredentials>, IToobitRestClientSpotApi
    {
        #region fields 
        protected override ErrorMapping ErrorMapping => ToobitErrors.Errors;
        protected override IRestMessageHandler MessageHandler { get; } = new ToobitRestMessageHandler(ToobitErrors.Errors);
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IToobitRestClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IToobitRestClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IToobitRestClientSpotApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "Toobit";
        #endregion

        #region constructor/destructor
        internal ToobitRestClientSpotApi(ILoggerFactory? loggerFactory, HttpClient? httpClient, ToobitRestOptions options)
            : base(loggerFactory, ToobitExchange.Metadata.Id, httpClient, options.Environment.RestClientAddress, options, options.SpotOptions)
        {
            RequestBodyFormat = RequestBodyFormat.FormData;
            RequestBodyEmptyContent = "";

            Account = new ToobitRestClientSpotApiAccount(this);
            ExchangeData = new ToobitRestClientSpotApiExchangeData(_logger, this);
            Trading = new ToobitRestClientSpotApiTrading(_logger, this);
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
            return result;
        }

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null) 
            => ToobitExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);

        /// <inheritdoc />
        public IToobitRestClientSpotApiShared SharedClient => this;

    }
}
