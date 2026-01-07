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
    internal partial class ToobitRestClientUsdtFuturesApi : RestApiClient, IToobitRestClientUsdtFuturesApi
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
        internal ToobitRestClientUsdtFuturesApi(ILogger logger, HttpClient? httpClient, ToobitRestOptions options)
            : base(logger, httpClient, options.Environment.RestClientAddress, options, options.UsdtFuturesOptions)
        {
            RequestBodyFormat = RequestBodyFormat.FormData;
            RequestBodyEmptyContent = "";

            Account = new ToobitRestClientUsdtFuturesApiAccount(this);
            ExchangeData = new ToobitRestClientUsdtFuturesApiExchangeData(logger, this);
            Trading = new ToobitRestClientUsdtFuturesApiTrading(logger, this);
        }
        #endregion

        /// <inheritdoc />
        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor(SerializerOptions.WithConverters(ToobitExchange._serializerContext));
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(ToobitExchange._serializerContext));


        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new ToobitAuthenticationProvider(credentials);

        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult> SendToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);

            // Optional response checking

            return result;
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            var result = await base.SendAsync<T>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);

            // Optional response checking

            return result;
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null) 
            => ToobitExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);

        /// <inheritdoc />
        public IToobitRestClientUsdtFuturesApiShared SharedClient => this;

    }
}
