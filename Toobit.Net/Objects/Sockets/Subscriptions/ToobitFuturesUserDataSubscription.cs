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
    internal class ToobitFuturesUserDataSubscription : Subscription<object, object>
    {
        private readonly Action<DataEvent<ToobitAccountUpdate>>? _accountHandler;
        private readonly Action<DataEvent<ToobitFuturesOrderUpdate[]>>? _orderHandler;
        private readonly Action<DataEvent<ToobitPositionUpdate[]>>? _positionHandler;
        private readonly Action<DataEvent<ToobitUserTradeUpdate[]>>? _userTradeHandler;

        /// <summary>
        /// ctor
        /// </summary>
        public ToobitFuturesUserDataSubscription(
            ILogger logger,
            Action<DataEvent<ToobitAccountUpdate>>? accountHandler = null,
            Action<DataEvent<ToobitFuturesOrderUpdate[]>>? orderHandler = null,
            Action<DataEvent<ToobitPositionUpdate[]>>? positionHandler = null,
            Action<DataEvent<ToobitUserTradeUpdate[]>>? tradeHandler = null) : base(logger, false)
        {
            _accountHandler = accountHandler;
            _orderHandler = orderHandler;
            _positionHandler = positionHandler;
            _userTradeHandler = tradeHandler;

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<ToobitAccountUpdate[]>(MessageLinkType.Full, "outboundContractAccountInfo", HandleAccountInfo),
                new MessageHandlerLink<ToobitPositionUpdate[]>(MessageLinkType.Full, "outboundContractPositionInfo", HandlePositionUpdate),
                new MessageHandlerLink<ToobitFuturesOrderUpdate[]>(MessageLinkType.Full, "contractExecutionReport", HandleOrderUpdate),
                new MessageHandlerLink<ToobitUserTradeUpdate[]>(MessageLinkType.Full, "ticketInfo", HandleUserTradeUpdate)
                ]);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection) => null;

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection) => null;


        public CallResult HandleAccountInfo(SocketConnection connection, DataEvent<ToobitAccountUpdate[]> message)
        {
            _accountHandler?.Invoke(message.As(message.Data.First(), "FuturesAccount", null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Any() ? message.Data.Max(x => x.EventTime) : null));
            return CallResult.SuccessResult;
        }

        public CallResult HandleOrderUpdate(SocketConnection connection, DataEvent<ToobitFuturesOrderUpdate[]> message)
        {
            _orderHandler?.Invoke(message.As(message.Data, "FuturesOrder", null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Any() ? message.Data.Max(x => x.EventTime) : null));
            return CallResult.SuccessResult;
        }

        public CallResult HandleUserTradeUpdate(SocketConnection connection, DataEvent<ToobitUserTradeUpdate[]> message)
        {
            _userTradeHandler?.Invoke(message.As(message.Data, "UserTrade", null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Any() ? message.Data.Max(x => x.EventTime) : null));
            return CallResult.SuccessResult;
        }

        public CallResult HandlePositionUpdate(SocketConnection connection, DataEvent<ToobitPositionUpdate[]> message)
        {
            _positionHandler?.Invoke(message.As(message.Data, "FuturesPosition", null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Any() ? message.Data.Max(x => x.EventTime) : null));
            return CallResult.SuccessResult;
        }
    }
}
