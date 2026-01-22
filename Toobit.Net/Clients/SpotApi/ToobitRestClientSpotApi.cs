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
    internal partial class ToobitRestClientSpotApi : RestApiClient, IToobitRestClientSpotApi
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
        internal ToobitRestClientSpotApi(ILogger logger, HttpClient? httpClient, ToobitRestOptions options)
            : base(logger, httpClient, options.Environment.RestClientAddress, options, options.SpotOptions)
        {
            RequestBodyFormat = RequestBodyFormat.FormData;
            RequestBodyEmptyContent = "";

            Account = new ToobitRestClientSpotApiAccount(this);
            ExchangeData = new ToobitRestClientSpotApiExchangeData(logger, this);
            Trading = new ToobitRestClientSpotApiTrading(logger, this);
        }
        #endregion

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
            return result;
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            var result = await base.SendAsync<T>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null) 
            => ToobitExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);

        /// <inheritdoc />
        public IToobitRestClientSpotApiShared SharedClient => this;

    }
}
