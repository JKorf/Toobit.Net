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

            MessageRouter = MessageRouter.Create([
                MessageRoute<ToobitAccountUpdate[]>.CreateWithoutTopicFilter("outboundContractAccountInfo", HandleAccountInfo),
                MessageRoute<ToobitPositionUpdate[]>.CreateWithoutTopicFilter("outboundContractPositionInfo", HandlePositionUpdate),
                MessageRoute<ToobitFuturesOrderUpdate[]>.CreateWithoutTopicFilter("contractExecutionReport", HandleOrderUpdate),
                MessageRoute<ToobitUserTradeUpdate[]>.CreateWithoutTopicFilter("ticketInfo", HandleUserTradeUpdate)
                ]);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection) => null;

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection) => null;


        public CallResult HandleAccountInfo(SocketConnection connection, DateTime receiveTime, string? originalData, ToobitAccountUpdate[] message)
        {
            _accountHandler?.Invoke(
                    new DataEvent<ToobitAccountUpdate>(message.First(), receiveTime, originalData)
                        .WithStreamId("FuturesAccount")
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(message.Length > 0 ? message.Max(x => x.EventTime) : null)
                );
            
            return CallResult.SuccessResult;
        }

        public CallResult HandleOrderUpdate(SocketConnection connection, DateTime receiveTime, string? originalData, ToobitFuturesOrderUpdate[] message)
        {
            _orderHandler?.Invoke(
                    new DataEvent<ToobitFuturesOrderUpdate[]>(message, receiveTime, originalData)
                        .WithStreamId("FuturesOrder")
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(message.Length > 0 ? message.Max(x => x.EventTime) : null)
                );
            
            return CallResult.SuccessResult;
        }

        public CallResult HandleUserTradeUpdate(SocketConnection connection, DateTime receiveTime, string? originalData, ToobitUserTradeUpdate[] message)
        {
            _userTradeHandler?.Invoke(
                    new DataEvent<ToobitUserTradeUpdate[]>(message, receiveTime, originalData)
                        .WithStreamId("UserTrade")
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(message.Length > 0 ? message.Max(x => x.EventTime) : null)
                );

            return CallResult.SuccessResult;
        }

        public CallResult HandlePositionUpdate(SocketConnection connection, DateTime receiveTime, string? originalData, ToobitPositionUpdate[] message)
        {
            _positionHandler?.Invoke(
                    new DataEvent<ToobitPositionUpdate[]>(message, receiveTime, originalData)
                        .WithStreamId("FuturesPosition")
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(message.Length > 0 ? message.Max(x => x.EventTime) : null)
                );

            return CallResult.SuccessResult;
        }
    }
}
