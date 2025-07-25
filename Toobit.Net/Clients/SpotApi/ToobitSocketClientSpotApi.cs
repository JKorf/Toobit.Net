using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Toobit.Net.Enums;
using Toobit.Net.Interfaces.Clients.SpotApi;
using Toobit.Net.Objects.Models;
using Toobit.Net.Objects.Options;
using Toobit.Net.Objects.Sockets;
using Toobit.Net.Objects.Sockets.Subscriptions;

namespace Toobit.Net.Clients.SpotApi
{
    /// <summary>
    /// Client providing access to the Toobit Spot websocket Api
    /// </summary>
    internal partial class ToobitSocketClientSpotApi : SocketApiClient, IToobitSocketClientSpotApi
    {
        #region fields
        private static readonly MessagePath _topicPath = MessagePath.Get().Property("topic");
        private static readonly MessagePath _pongPath = MessagePath.Get().Property("pong");
        private static readonly MessagePath _pingPath = MessagePath.Get().Property("ping");
        private static readonly MessagePath _symbolPath = MessagePath.Get().Property("symbol");
        private static readonly MessagePath _userEventPath = MessagePath.Get().Index(0).Property("e");
        private static readonly MessagePath _intervalPath = MessagePath.Get().Property("params").Property("klineType");
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal ToobitSocketClientSpotApi(ILogger logger, ToobitSocketOptions options) :
            base(logger, options.Environment.SocketClientAddress!, options, options.SpotOptions)
        {
            RegisterPeriodicQuery("Ping",
                TimeSpan.FromSeconds(30),
                x => new PingQuery(),
                (connection, result) =>
                {
                    if (result.Error?.Message.Equals("Query timeout") == true)
                    {
                        // Ping timeout, reconnect
                        _logger.LogWarning("[Sckt {SocketId}] Ping response timeout, reconnecting", connection.SocketId);
                        _ = connection.TriggerReconnectAsync();
                    }
                });

            AddSystemSubscription(new ToobitPingSubscription(_logger));

            RateLimiter = ToobitExchange.RateLimiter.ToobitSocket;
        }
        #endregion

        /// <inheritdoc />
        protected override IByteMessageAccessor CreateAccessor(WebSocketMessageType type) => new SystemTextJsonByteMessageAccessor(SerializerOptions.WithConverters(ToobitExchange._serializerContext));
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(ToobitExchange._serializerContext));

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new ToobitAuthenticationProvider(credentials);

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<ToobitTradeUpdate[]>> onMessage, CancellationToken ct = default)
            => SubscribeToTradeUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<ToobitTradeUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new ToobitSubscription<ToobitTradeUpdate[]>(_logger, symbols.ToArray(), "trade", null, onMessage, false);
            return await SubscribeAsync(BaseAddress.AppendPath("/quote/ws/v1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<ToobitTickerUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToTickerUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<ToobitTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new ToobitSubscription<ToobitTickerUpdate[]>(_logger, symbols.ToArray(), "realtimes", null, x => onMessage(x.As(x.Data.First())), false);
            return await SubscribeAsync(BaseAddress.AppendPath("/quote/ws/v1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<ToobitKlineUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToKlineUpdatesAsync([symbol], interval, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<ToobitKlineUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new ToobitSubscription<ToobitKlineUpdate[]>(_logger, symbols.ToArray(), "kline", interval, x => onMessage(x.As(x.Data.First())), false);
            return await SubscribeAsync(BaseAddress.AppendPath("/quote/ws/v1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, Action<DataEvent<ToobitOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToPartialOrderBookUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<ToobitOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new ToobitSubscription<ToobitOrderBookUpdate[]>(_logger, symbols.ToArray(), "depth", null, x => onMessage(x.As(x.Data.First())), false);
            return await SubscribeAsync(BaseAddress.AppendPath("/quote/ws/v1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<ToobitOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToOrderBookUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<ToobitOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new ToobitSubscription<ToobitOrderBookUpdate[]>(_logger, symbols.ToArray(), "diffDepth", null, x => onMessage(x.As(x.Data.First())), false);
            return await SubscribeAsync(BaseAddress.AppendPath("/quote/ws/v1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<DataEvent<ToobitAccountUpdate>>? onAccountMessage = null,
            Action<DataEvent<ToobitOrderUpdate[]>>? onOrderMessage = null,
            Action<DataEvent<ToobitUserTradeUpdate[]>>? onUserTradeMessage = null,
            CancellationToken ct = default)
        {
            var subscription = new ToobitUserDataSubscription(_logger, onAccountMessage, onOrderMessage, onUserTradeMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("/api/v1/ws/" + listenKey), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            var topic = message.GetValue<string?>(_topicPath);
            if (topic == null)
            {
                var userEvent = message.GetValue<string?>(_userEventPath);
                if (userEvent != null)
                    return userEvent;

                if (message.GetValue<long>(_pongPath) != 0)
                    return "pong";
                else if (message.GetValue<long>(_pingPath) != 0)
                    return "ping";
                else
                    return null;
            }

            var symbol = message.GetValue<string?>(_symbolPath);;
            if (topic!.Equals("kline", StringComparison.Ordinal))
            {
                var interval = message.GetValue<string>(_intervalPath);
                return symbol == null ? topic : topic + "-" + symbol + "-" + interval;
            }

            return symbol == null ? topic : topic + "-" + symbol;
        }

        /// <inheritdoc />
        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection) => Task.FromResult<Query?>(null);

        /// <inheritdoc />
        public IToobitSocketClientSpotApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => ToobitExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);
    }
}
