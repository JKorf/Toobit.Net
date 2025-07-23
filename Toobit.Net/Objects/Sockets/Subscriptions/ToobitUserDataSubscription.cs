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
using Toobit.Net.Objects.Models;

namespace Toobit.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class ToobitUserDataSubscription : Subscription<object, object>
    {
        private readonly Action<DataEvent<ToobitAccountUpdate>>? _accountHandler;
        private readonly Action<DataEvent<ToobitOrderUpdate[]>>? _orderHandler;
        private readonly Action<DataEvent<ToobitUserTradeUpdate[]>>? _userTradeHandler;

        /// <summary>
        /// ctor
        /// </summary>
        public ToobitUserDataSubscription(
            ILogger logger,
            Action<DataEvent<ToobitAccountUpdate>>? accountHandler = null,
            Action<DataEvent<ToobitOrderUpdate[]>>? orderHandler = null,
            Action<DataEvent<ToobitUserTradeUpdate[]>>? tradeHandler = null) : base(logger, false)
        {
            _accountHandler = accountHandler;
            _orderHandler = orderHandler;
            _userTradeHandler = tradeHandler;

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<ToobitAccountUpdate[]>(MessageLinkType.Full, "outboundAccountInfo", HandleAccountInfo),
                new MessageHandlerLink<ToobitOrderUpdate[]>(MessageLinkType.Full, "executionReport", HandleOrderUpdate),
                new MessageHandlerLink<ToobitUserTradeUpdate[]>(MessageLinkType.Full, "ticketInfo", HandleUserTrade)
                ]);
        }

        /// <inheritdoc />
        public override Query? GetSubQuery(SocketConnection connection) => null;

        /// <inheritdoc />
        public override Query? GetUnsubQuery() => null;

        public CallResult HandleAccountInfo(SocketConnection connection, DataEvent<ToobitAccountUpdate[]> message)
        {
            _accountHandler?.Invoke(message.As(message.Data.First(), "SpotAccount", null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Any() ? message.Data.Max(x => x.EventTime) : null));
            return CallResult.SuccessResult;
        }

        public CallResult HandleOrderUpdate(SocketConnection connection, DataEvent<ToobitOrderUpdate[]> message)
        {
            _orderHandler?.Invoke(message.As(message.Data, "SpotOrder", null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Any() ? message.Data.Max(x => x.EventTime) : null));
            return CallResult.SuccessResult;
        }

        public CallResult HandleUserTrade(SocketConnection connection, DataEvent<ToobitUserTradeUpdate[]> message)
        {
            _userTradeHandler?.Invoke(message.As(message.Data, "SpotUserTrade", null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Any() ? message.Data.Max(x => x.EventTime) : null));
            return CallResult.SuccessResult;
        }
    }
}
