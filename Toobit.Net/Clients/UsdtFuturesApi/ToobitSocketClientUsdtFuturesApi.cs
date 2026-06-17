using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Interfaces;
using CryptoExchange.Net.TokenManagement;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Toobit.Net.Clients.MessageHandlers;
using Toobit.Net.Enums;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;
using Toobit.Net.Objects.Models;
using Toobit.Net.Objects.Options;
using Toobit.Net.Objects.Sockets;
using Toobit.Net.Objects.Sockets.Subscriptions;

namespace Toobit.Net.Clients.UsdtFuturesApi
{
    /// <summary>
    /// Client providing access to the Toobit UsdtFutures websocket Api
    /// </summary>
    internal partial class ToobitSocketClientUsdtFuturesApi : SocketApiClient<ToobitEnvironment, ToobitAuthenticationProvider, ToobitCredentials>, IToobitSocketClientUsdtFuturesApi
    {
        #region fields
        private readonly TimeSpan _waitForErrorTimeout;
        private readonly ILoggerFactory? _loggerFactory;
        private ToobitRestClient? _tokenClient;
        internal TokenManager TokenManager { get; }
        private ToobitRestClient TokenClient
        {
            get
            {
                if (_tokenClient == null)
                {
                    _tokenClient = new ToobitRestClient(null, _loggerFactory, Options.Create(new ToobitRestOptions
                    {
                        ApiCredentials = ApiCredentials,
                        Environment = ClientOptions.Environment,
                        Proxy = ClientOptions.Proxy,
                        OutputOriginalData = ClientOptions.OutputOriginalData
                    }));
                }

                return _tokenClient;
            }
        }
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal ToobitSocketClientUsdtFuturesApi(ILoggerFactory? loggerFactory, ToobitSocketOptions options) :
            base(loggerFactory, ToobitExchange.Metadata.Id, options.Environment.SocketClientAddress!, options, options.UsdtFuturesOptions)
        {
            _loggerFactory = loggerFactory;
            _waitForErrorTimeout = options.SubscribeMaxWaitForError;

            RegisterPeriodicQuery("Ping",
                TimeSpan.FromSeconds(30),
                x => new PingQuery(),
                (connection, result) =>
                {
                    if (result.Error?.ErrorType == ErrorType.Timeout)
                    {
                        // Ping timeout, reconnect
                        _logger.LogWarning("[Sckt {SocketId}] Ping response timeout, reconnecting", connection.SocketId);
                        _ = connection.TriggerReconnectAsync();
                    }
                });

            AddSystemSubscription(new ToobitPingSubscription(_logger));

            RateLimiter = ToobitExchange.RateLimiter.ToobitSocket;
            TokenManager = new TokenManager(
                ToobitExchange.Metadata.Id,
                loggerFactory,
                TimeSpan.FromMinutes(30),
                TimeSpan.FromMinutes(60),
                startToken: StartListenKeyAsync,
                keepAliveToken: KeepAliveListenKeyAsync,
                stopToken: StopListenKeyAsync);
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(ToobitExchange._serializerContext));
        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType) => new ToobitSocketFuturesMessageHandler();

        /// <inheritdoc />
        protected override ToobitAuthenticationProvider CreateAuthenticationProvider(ToobitCredentials credentials)
            => new ToobitAuthenticationProvider(credentials);

        /// <inheritdoc />
        public IToobitSocketClientUsdtFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<ToobitTradeUpdate[]>> onMessage, CancellationToken ct = default)
            => SubscribeToTradeUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<ToobitTradeUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, SocketUpdate<ToobitTradeUpdate[]>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.SendTime);

                onMessage(
                    new DataEvent<ToobitTradeUpdate[]>(ToobitExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithUpdateType(data.First ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                        .WithDataTimestamp(data.SendTime, GetTimeOffset())
                        .WithStreamId(data.Topic)
                        .WithSymbol(data.Symbol)
                    );
            });

            var subscription = new ToobitSubscription<ToobitTradeUpdate[]>(_logger, this, symbols.ToArray(), "trade", null, internalHandler, false, _waitForErrorTimeout);
            return await SubscribeAsync(BaseAddress.AppendPath("/quote/ws/v1"), subscription, ct).ConfigureAwait(false);
        }

        ///// <inheritdoc />
        //public Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<ToobitMarkPriceUpdate>> onMessage, CancellationToken ct = default)
        //    => SubscribeToMarkPriceUpdatesAsync([symbol], onMessage, ct);

        ///// <inheritdoc />
        //public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<ToobitMarkPriceUpdate>> onMessage, CancellationToken ct = default)
        //{
        //    var internalHandler = new Action<DateTime, string?, SocketUpdate<ToobitTradeUpdate[]>>((receiveTime, originalData, data) =>
        //    {
        //        onMessage(
        //            new DataEvent<ToobitTradeUpdate[]>(data.Data, receiveTime, originalData)
        //                .WithUpdateType(data.First ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
        //                .WithDataTimestamp(data.SendTime)
        //                .WithStreamId(data.Topic)
        //                .WithSymbol(data.Symbol)
        //            );
        //    });

        //    var subscription = new ToobitMarkPriceSubscription(_logger, this, symbols.ToArray(), onMessage, false, _waitForErrorTimeout);
        //    return await SubscribeAsync(BaseAddress.AppendPath("/quote/ws/v1"), subscription, ct).ConfigureAwait(false);
        //}

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<ToobitKlineUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToKlineUpdatesAsync([symbol], interval, onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<ToobitKlineUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, SocketUpdate<ToobitKlineUpdate[]>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.SendTime);

                if (data.Data.Length > 0)
                {
                    onMessage(
                        new DataEvent<ToobitKlineUpdate>(ToobitExchange.ExchangeName, data.Data.First(), receiveTime, originalData)
                            .WithUpdateType(data.First ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                            .WithDataTimestamp(data.SendTime, GetTimeOffset())
                            .WithStreamId(data.Topic)
                            .WithSymbol(data.Symbol)
                        );
                }
            });

            var subscription = new ToobitSubscription<ToobitKlineUpdate[]>(_logger, this, symbols.ToArray(), "kline", interval, internalHandler, false, _waitForErrorTimeout);
            return await SubscribeAsync(BaseAddress.AppendPath("/quote/ws/v1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, Action<DataEvent<ToobitOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToPartialOrderBookUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<ToobitOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, SocketUpdate<ToobitOrderBookUpdate[]>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.SendTime);

                if (data.Data.Length > 0)
                {
                    onMessage(
                        new DataEvent<ToobitOrderBookUpdate>(ToobitExchange.ExchangeName, data.Data.First(), receiveTime, originalData)
                            .WithUpdateType(data.First ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                            .WithDataTimestamp(data.SendTime, GetTimeOffset())
                            .WithStreamId(data.Topic)
                            .WithSymbol(data.Symbol)
                        );
                }
            });

            var subscription = new ToobitSubscription<ToobitOrderBookUpdate[]>(_logger, this, symbols.ToArray(), "depth", null, internalHandler, false, _waitForErrorTimeout);
            return await SubscribeAsync(BaseAddress.AppendPath("/quote/ws/v1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<ToobitOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToOrderBookUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<ToobitOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, SocketUpdate<ToobitOrderBookUpdate[]>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.SendTime);

                if (data.Data.Length > 0)
                {
                    onMessage(
                        new DataEvent<ToobitOrderBookUpdate>(ToobitExchange.ExchangeName, data.Data.First(), receiveTime, originalData)
                            .WithUpdateType(data.First ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                            .WithDataTimestamp(data.SendTime, GetTimeOffset())
                            .WithStreamId(data.Topic)
                            .WithSymbol(data.Symbol)
                        );
                }
            });

            var subscription = new ToobitSubscription<ToobitOrderBookUpdate[]>(_logger, this, symbols.ToArray(), "diffDepth", null, internalHandler, false, _waitForErrorTimeout);
            return await SubscribeAsync(BaseAddress.AppendPath("/quote/ws/v1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<ToobitTickerUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToTickerUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<ToobitTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, SocketUpdate<ToobitTickerUpdate[]>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.SendTime);

                if (data.Data.Length > 0)
                {
                    onMessage(
                        new DataEvent<ToobitTickerUpdate>(ToobitExchange.ExchangeName, data.Data.First(), receiveTime, originalData)
                            .WithUpdateType(data.First ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                            .WithDataTimestamp(data.SendTime, GetTimeOffset())
                            .WithStreamId(data.Topic)
                            .WithSymbol(data.Symbol)
                        );
                }
            });

            var subscription = new ToobitSubscription<ToobitTickerUpdate[]>(_logger, this, symbols.ToArray(), "realtimes", null, internalHandler, false, _waitForErrorTimeout);
            return await SubscribeAsync(BaseAddress.AppendPath("/quote/ws/v1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<ToobitIndexUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToIndexPriceUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<ToobitIndexUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, SocketUpdate<ToobitIndexUpdate[]>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.SendTime);

                if (data.Data.Length > 0)
                {
                    onMessage(
                        new DataEvent<ToobitIndexUpdate>(ToobitExchange.ExchangeName, data.Data.First(), receiveTime, originalData)
                            .WithUpdateType(data.First ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                            .WithDataTimestamp(data.SendTime, GetTimeOffset())
                            .WithStreamId(data.Topic)
                            .WithSymbol(data.Symbol)
                        );
                }
            });
            var subscription = new ToobitSubscription<ToobitIndexUpdate[]>(_logger, this, symbols.ToArray(), "index", null, internalHandler, false, _waitForErrorTimeout);
            return await SubscribeAsync(BaseAddress.AppendPath("/quote/ws/v1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            Action<DataEvent<ToobitAccountUpdate>>? onAccountMessage = null,
            Action<DataEvent<ToobitFuturesOrderUpdate[]>>? onOrderMessage = null,
            Action<DataEvent<ToobitPositionUpdate[]>>? onPositionMessage = null,
            Action<DataEvent<ToobitUserTradeUpdate[]>>? onUserTradeMessage = null,
            CancellationToken ct = default)
            => SubscribeToUserDataUpdatesAsync(null, onAccountMessage, onOrderMessage, onPositionMessage, onUserTradeMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string? listenKey,
            Action<DataEvent<ToobitAccountUpdate>>? onAccountMessage = null,
            Action<DataEvent<ToobitFuturesOrderUpdate[]>>? onOrderMessage = null,
            Action<DataEvent<ToobitPositionUpdate[]>>? onPositionMessage = null,
            Action<DataEvent<ToobitUserTradeUpdate[]>>? onUserTradeMessage = null,
            CancellationToken ct = default)
        {
            if (listenKey == null && !Authenticated)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, new NoApiCredentialsError());

            TokenLease? lease = null;
            if (listenKey == null)
            {
                var leaseResult = await TokenManager.AcquireAsync(new TokenScope(
                    ToobitExchange.Metadata.Id,
                    EnvironmentName,
                    "Futures",
                    ApiCredentials!.Key), ct).ConfigureAwait(false);
                if (!leaseResult.Success)
                    return WebSocketResult.Fail<UpdateSubscription>(Exchange, leaseResult.Error);

                lease = leaseResult.Data;
            }

            var lk = listenKey ?? lease!.Token.Token;

            var subscription = new ToobitFuturesUserDataSubscription(_logger, this, onAccountMessage, onOrderMessage, onPositionMessage, onUserTradeMessage)
            {
                TokenLease = lease
            };
            var result = await SubscribeAsync(BaseAddress.AppendPath("/api/v1/ws/" + listenKey), subscription, ct).ConfigureAwait(false);
            if (!result.Success && lease != null)
                await lease.ReleaseAsync().ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => ToobitExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);

        protected override async Task<Uri?> GetReconnectUriAsync(ISocketConnection connection)
        {
            if (!connection.HasAuthenticatedSubscription)
                return await base.GetReconnectUriAsync(connection).ConfigureAwait(false);


            var subscriptions = ((SocketConnection)connection).Subscriptions.Where(x => x.TokenLease != null).ToList();

            // We have authenticated subscription via the token manager
            var scope = new TokenScope(
                    ToobitExchange.Metadata.Id,
                    EnvironmentName,
                    "Futures",
                    ApiCredentials!.Key);

            var token = await TokenManager.AcquireAndReplaceAsync(subscriptions[0], scope).ConfigureAwait(false);
            if (!token.Success)
                return null;

            return new Uri(BaseAddress.AppendPath("/api/v1/ws/" + token.Data.Token.Token));
        }

        private async Task<CallResult<string>> StartListenKeyAsync(TokenScope tokenScope, CancellationToken ct)
        {
            var result = await TokenClient.UsdtFuturesApi.Account.StartUserStreamAsync(ct).ConfigureAwait(false);
            if (!result.Success)
                return CallResult.Fail<string>(result.Error);

            return CallResult.Ok(result.Data);
        }

        private async Task<CallResult> KeepAliveListenKeyAsync(TokenInfo token, CancellationToken ct)
        {
            var result = await TokenClient.UsdtFuturesApi.Account.KeepAliveUserStreamAsync(token.Token, ct).ConfigureAwait(false);
            if (!result.Success)
                return CallResult.Fail<string>(result.Error);

            return CallResult.Ok();
        }

        private async Task<CallResult> StopListenKeyAsync(TokenInfo token, CancellationToken ct)
        {
            var result = await TokenClient.UsdtFuturesApi.Account.StopUserStreamAsync(token.Token, ct).ConfigureAwait(false);
            if (!result.Success)
                return CallResult.Fail<string>(result.Error);

            return CallResult.Ok();
        }
    }
}
