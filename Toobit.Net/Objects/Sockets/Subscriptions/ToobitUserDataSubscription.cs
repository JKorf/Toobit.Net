using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Toobit.Net.Objects.Models;

namespace Toobit.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class ToobitUserDataSubscription : Subscription
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

            MessageRouter = MessageRouter.Create([
                MessageRoute<ToobitAccountUpdate[]>.CreateWithoutTopicFilter("outboundAccountInfo", HandleAccountInfo),
                MessageRoute<ToobitOrderUpdate[]>.CreateWithoutTopicFilter("executionReport", HandleOrderUpdate),
                MessageRoute<ToobitUserTradeUpdate[]>.CreateWithoutTopicFilter("ticketInfo", HandleUserTrade)
                ]);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection) => null;

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection) => null;

        public CallResult HandleAccountInfo(SocketConnection connection, DateTime receiveTime, string? originalData, ToobitAccountUpdate[] message)
        {
            _accountHandler?.Invoke(
                    new DataEvent<ToobitAccountUpdate>(ToobitExchange.ExchangeName, message.First(), receiveTime, originalData)
                        .WithStreamId("SpotAccount")
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(message.Length > 0 ? message.Max(x => x.EventTime) : null)
                );

            return CallResult.SuccessResult;
        }

        public CallResult HandleOrderUpdate(SocketConnection connection, DateTime receiveTime, string? originalData, ToobitOrderUpdate[] message)
        {
            _orderHandler?.Invoke(
                    new DataEvent<ToobitOrderUpdate[]>(ToobitExchange.ExchangeName, message, receiveTime, originalData)
                        .WithStreamId("SpotOrder")
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(message.Length > 0 ? message.Max(x => x.EventTime) : null)
                );

            return CallResult.SuccessResult;
        }

        public CallResult HandleUserTrade(SocketConnection connection, DateTime receiveTime, string? originalData, ToobitUserTradeUpdate[] message)
        {
            _userTradeHandler?.Invoke(
                    new DataEvent<ToobitUserTradeUpdate[]>(ToobitExchange.ExchangeName, message, receiveTime, originalData)
                        .WithStreamId("SpotUserTrade")
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(message.Length > 0 ? message.Max(x => x.EventTime) : null)
                );

            return CallResult.SuccessResult;
        }
    }
}
