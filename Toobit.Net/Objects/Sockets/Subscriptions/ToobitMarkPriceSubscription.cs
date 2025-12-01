//using CryptoExchange.Net.Clients;
//using CryptoExchange.Net.Converters.MessageParsing;
//using CryptoExchange.Net.Converters.SystemTextJson;
//using CryptoExchange.Net.Interfaces;
//using CryptoExchange.Net.Objects;
//using CryptoExchange.Net.Objects.Sockets;
//using CryptoExchange.Net.Sockets;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Toobit.Net.Enums;
//using Toobit.Net.Objects.Models;

//namespace Toobit.Net.Objects.Sockets.Subscriptions
//{
//    /// <inheritdoc />
//    internal class ToobitMarkPriceSubscription : Subscription<object, object>
//    {
//        private readonly TimeSpan _waitForErrorTimeout;
//        private readonly SocketApiClient _client;
//        private readonly Action<DataEvent<ToobitMarkPriceUpdate>> _handler;
//        private readonly string[] _symbols;
//        private readonly string _topic;

//        /// <summary>
//        /// ctor
//        /// </summary>
//        public ToobitMarkPriceSubscription(
//            ILogger logger,
//            SocketApiClient client,
//            string[] symbols,
//            Action<DataEvent<ToobitMarkPriceUpdate>> handler,
//            bool auth,
//            TimeSpan waitForErrorTimeout) : base(logger, auth)
//        {
//            _client = client;
//            _handler = handler;
//            _symbols = symbols;
//            _waitForErrorTimeout = waitForErrorTimeout;
//            _topic = "markPrice";

//            // Subscription doesn't seem to work although it's implemented as documented
//            MessageMatcher = MessageMatcher.Create(
//                symbols.Select(x => new MessageHandlerLink<SocketUpdate<ToobitMarkPriceUpdate[]>>( "markPrice-" + x, DoHandleMessageArray)).ToArray()
//                );
//        }

//        /// <inheritdoc />
//        protected override Query? GetSubQuery(SocketConnection connection)
//        {
//            var request = new SocketRequest
//            {
//                Symbols = string.Join(",", _symbols),
//                Topic = _topic,
//                Event = "sub",
//                Parameters = new Dictionary<string, object>
//                {
//                    { "binary", false }
//                }
//            };
//            return new ToobitQuery<object>(_client, request, Authenticated, _waitForErrorTimeout);
//        }

//        /// <inheritdoc />
//        protected override Query? GetUnsubQuery(SocketConnection connection)
//        {
//            var request = new SocketRequest
//            {
//                Symbols = string.Join(",", _symbols),
//                Topic = _topic,
//                Event = "cancel",
//                Parameters = new Dictionary<string, object>
//                {
//                    { "binary", false }
//                }
//            };
//            return new ToobitQuery<object>(_client, request, Authenticated, _waitForErrorTimeout);
//        }

//        /// <inheritdoc />
//        public CallResult DoHandleMessageSingle(SocketConnection connection, DataEvent<SocketUpdate<ToobitMarkPriceUpdate>> message)
//        {
//            _handler.Invoke(message.As(message.Data.Data, message.Data.Topic, message.Data.Symbol, message.Data.First ? SocketUpdateType.Snapshot : SocketUpdateType.Update).WithDataTimestamp(message.Data.SendTime));
//            return new CallResult(null);
//        }

//        /// <inheritdoc />
//        public CallResult DoHandleMessageArray(SocketConnection connection, DataEvent<SocketUpdate<ToobitMarkPriceUpdate[]>> message)
//        {
//            _handler.Invoke(message.As(message.Data.Data.Single(), message.Data.Topic, message.Data.Symbol, message.Data.First ? SocketUpdateType.Snapshot : SocketUpdateType.Update).WithDataTimestamp(message.Data.SendTime));
//            return new CallResult(null);
//        }
//    }
//}
