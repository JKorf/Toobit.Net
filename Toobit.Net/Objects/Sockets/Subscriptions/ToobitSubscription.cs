using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Toobit.Net.Enums;
using Toobit.Net.Objects.Models;

namespace Toobit.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class ToobitSubscription<T> : Subscription<object, object>
    {
        private readonly TimeSpan _waitForErrorTimeout;
        private readonly SocketApiClient _client;
        private readonly Action<DateTime, string?, SocketUpdate<T>> _handler;
        private readonly string[]? _symbols;
        private readonly string _topic;
        private readonly KlineInterval? _interval;

        /// <summary>
        /// ctor
        /// </summary>
        public ToobitSubscription(
            ILogger logger,
            SocketApiClient client,
            string[]? symbols,
            string topic,
            KlineInterval? interval,
            Action<DateTime, string?, SocketUpdate<T>> handler,
            bool auth,
            TimeSpan waitForErrorTimeout) : base(logger, auth)
        {
            _client = client;
            _handler = handler;
            _symbols = symbols;
            _topic = topic + (interval == null ? "" : ("_" + EnumConverter.GetString(interval.Value)));
            _interval = interval;
            _waitForErrorTimeout = waitForErrorTimeout;
            if (symbols?.Any() == true)
                MessageMatcher = MessageMatcher.Create<SocketUpdate<T>>(symbols.Select(x => topic + "-" + x + (_interval == null ? "" : ("-" + EnumConverter.GetString(_interval.Value)))), DoHandleMessage);
            else
                MessageMatcher = MessageMatcher.Create<SocketUpdate<T>>(topic, DoHandleMessage);

            MessageRouter = MessageRouter.CreateWithOptionalTopicFilters<SocketUpdate<T>>(topic, symbols?.Select(x => _interval == null ? x : x + EnumConverter.GetString(_interval.Value)), DoHandleMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
        {
            var request = new SocketRequest
            {
                Symbols = _symbols == null ? null : string.Join(",", _symbols),
                Topic = _topic,
                Event = "sub",
                Parameters = new Dictionary<string, object>
                {
                    { "binary", false }
                }
            };
            if (_interval != null)
                request.Parameters.Add("klineType", EnumConverter.GetString(_interval.Value));

            return new ToobitQuery<object>(_client, request, Authenticated, _waitForErrorTimeout);
        }

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            var request = new SocketRequest
            {
                Symbols = _symbols == null ? null : string.Join(",", _symbols),
                Topic = _topic,
                Event = "cancel",
                Parameters = new Dictionary<string, object>
                {
                    { "binary", false }
                }
            };
            if (_interval != null)
                request.Parameters.Add("klineType", EnumConverter.GetString(_interval.Value));

            return new ToobitQuery<object>(_client, request, Authenticated, _waitForErrorTimeout);
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, SocketUpdate<T> message)
        {
            //_handler.Invoke(message.As(message.Data.Data, message.Data.Topic, message.Data.Symbol, message.Data.First ? SocketUpdateType.Snapshot : SocketUpdateType.Update).WithDataTimestamp(message.Data.SendTime));
            _handler.Invoke(receiveTime, originalData, message);
            return new CallResult(null);
        }
    }
}
