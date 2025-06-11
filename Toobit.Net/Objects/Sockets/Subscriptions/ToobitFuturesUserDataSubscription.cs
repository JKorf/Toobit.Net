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
    internal class ToobitFuturesUserDataSubscription : Subscription<object, object>
    {
        /// <inheritdoc />
        public override HashSet<string> ListenerIdentifiers { get; set; }

        private readonly MessagePath _typePath = MessagePath.Get().Index(0).Property("e");
        private readonly Action<DataEvent<ToobitAccountUpdate>>? _accountHandler;
        private readonly Action<DataEvent<ToobitFuturesOrderUpdate[]>>? _orderHandler;
        private readonly Action<DataEvent<ToobitPositionUpdate[]>>? _positionHandler;
        private readonly Action<DataEvent<ToobitUserTradeUpdate[]>>? _userTradeHandler;

        /// <inheritdoc />
        public override Type? GetMessageType(IMessageAccessor message)
        {
            var type = message.GetValue<string>(_typePath);
            if (type == "outboundContractAccountInfo")
                return typeof(ToobitAccountUpdate[]);
            if (type == "outboundContractPositionInfo")
                return typeof(ToobitPositionUpdate[]);
            else if (type == "contractExecutionReport")
                return typeof(ToobitFuturesOrderUpdate[]);
            else if (type == "ticketInfo")
                return typeof(ToobitUserTradeUpdate[]);

            return null;
        }

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

            ListenerIdentifiers = new HashSet<string> { "user" };
        }

        /// <inheritdoc />
        public override Query? GetSubQuery(SocketConnection connection) => null;

        /// <inheritdoc />
        public override Query? GetUnsubQuery() => null;

        /// <inheritdoc />
        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            if (message.Data is ToobitAccountUpdate[] accountUpdate)
                _accountHandler?.Invoke(message.As(accountUpdate.First(), "FuturesAccount", null, SocketUpdateType.Update).WithDataTimestamp(accountUpdate.Any() ? accountUpdate.Max(x => x.EventTime) : null));
            else if (message.Data is ToobitFuturesOrderUpdate[] orderUpdate)
                _orderHandler?.Invoke(message.As(orderUpdate, "FuturesOrder", null, SocketUpdateType.Update).WithDataTimestamp(orderUpdate.Any() ? orderUpdate.Max(x => x.EventTime) : null));
            else if (message.Data is ToobitPositionUpdate[] positionUpdate)
                _positionHandler?.Invoke(message.As(positionUpdate, "FuturesPosition", null, SocketUpdateType.Update).WithDataTimestamp(positionUpdate.Any() ? positionUpdate.Max(x => x.EventTime) : null));
            else if (message.Data is ToobitUserTradeUpdate[] userTradeUpdate)
                _userTradeHandler?.Invoke(message.As(userTradeUpdate, "UserTrade", null, SocketUpdateType.Update).WithDataTimestamp(userTradeUpdate.Any() ? userTradeUpdate.Max(x => x.EventTime) : null));

            return new CallResult(null);
        }
    }
}
