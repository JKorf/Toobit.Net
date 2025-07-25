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
        private readonly Action<DataEvent<T>> _handler;
        private readonly string[]? _symbols;
        private readonly string _topic;
        private readonly KlineInterval? _interval;

        /// <summary>
        /// ctor
        /// </summary>
        public ToobitSubscription(
            ILogger logger,
            string[]? symbols,
            string topic,
            KlineInterval? interval,
            Action<DataEvent<T>> handler,
            bool auth) : base(logger, auth)
        {
            _handler = handler;
            _symbols = symbols;
            _topic = topic + (interval == null ? "" : ("_" + EnumConverter.GetString(interval.Value)));
            _interval = interval;
            if (symbols?.Any() == true)
                MessageMatcher = MessageMatcher.Create<SocketUpdate<T>>(symbols.Select(x => topic + "-" + x + (_interval == null ? "" : ("-" + EnumConverter.GetString(_interval.Value)))), DoHandleMessage);
            else
                MessageMatcher = MessageMatcher.Create<SocketUpdate<T>>(topic, DoHandleMessage);
        }

        /// <inheritdoc />
        public override Query? GetSubQuery(SocketConnection connection)
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

            return new ToobitQuery<object>(request, Authenticated);
        }

        /// <inheritdoc />
        public override Query? GetUnsubQuery()
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

            return new ToobitQuery<object>(request, Authenticated);
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<SocketUpdate<T>> message)
        {
            _handler.Invoke(message.As(message.Data.Data, message.Data.Topic, message.Data.Symbol, message.Data.First ? SocketUpdateType.Snapshot : SocketUpdateType.Update).WithDataTimestamp(message.Data.SendTime));
            return new CallResult(null);
        }
    }
}
