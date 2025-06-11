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
    internal class ToobitMarkPriceSubscription : Subscription<object, object>
    {
        /// <inheritdoc />
        public override HashSet<string> ListenerIdentifiers { get; set; }

        private readonly Action<DataEvent<ToobitMarkPriceUpdate>> _handler;
        private readonly string[] _symbols;
        private readonly string _topic;

        /// <inheritdoc />
        public override Type? GetMessageType(IMessageAccessor message)
        {
            if (message.GetNodeType(MessagePath.Get().Property("data")) == NodeType.Array)
                return typeof(SocketUpdate<ToobitMarkPriceUpdate[]>);

            return typeof(SocketUpdate<ToobitMarkPriceUpdate>);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public ToobitMarkPriceSubscription(
            ILogger logger,
            string[] symbols,
            Action<DataEvent<ToobitMarkPriceUpdate>> handler,
            bool auth) : base(logger, auth)
        {
            _handler = handler;
            _symbols = symbols;
            _topic = "markPrice";
            ListenerIdentifiers = new HashSet<string>(symbols.Select(x => "markPrice-" + x));
        }

        /// <inheritdoc />
        public override Query? GetSubQuery(SocketConnection connection)
        {
            var request = new SocketRequest
            {
                Symbols = string.Join(",", _symbols),
                Topic = _topic,
                Event = "sub",
                Parameters = new Dictionary<string, object>
                {
                    { "binary", false }
                }
            };
            return new ToobitQuery<object>(request, Authenticated);
        }

        /// <inheritdoc />
        public override Query? GetUnsubQuery()
        {
            var request = new SocketRequest
            {
                Symbols = string.Join(",", _symbols),
                Topic = _topic,
                Event = "unsub",
                Parameters = new Dictionary<string, object>
                {
                    { "binary", false }
                }
            };
            return new ToobitQuery<object>(request, Authenticated);
        }

        /// <inheritdoc />
        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            if (message.Data is SocketUpdate<ToobitMarkPriceUpdate> singleUpdate)
                _handler.Invoke(message.As(singleUpdate.Data, singleUpdate.Topic, singleUpdate.Symbol, singleUpdate.First ? SocketUpdateType.Snapshot : SocketUpdateType.Update).WithDataTimestamp(singleUpdate.SendTime));
            else if(message.Data is SocketUpdate<ToobitMarkPriceUpdate[]> arrayUpdate)
                _handler.Invoke(message.As(arrayUpdate.Data.Single(), arrayUpdate.Topic, arrayUpdate.Symbol, arrayUpdate.First ? SocketUpdateType.Snapshot : SocketUpdateType.Update).WithDataTimestamp(arrayUpdate.SendTime));

            return new CallResult(null);
        }
    }
}
