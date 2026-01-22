using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Toobit.Net.Clients.UsdtFuturesApi;
using Toobit.Net.Objects.Models;

namespace Toobit.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class ToobitFuturesUserDataSubscription: Subscription
    {
        private readonly ToobitSocketClientUsdtFuturesApi _client;

        private readonly Action<DataEvent<ToobitAccountUpdate>>? _accountHandler;
        private readonly Action<DataEvent<ToobitFuturesOrderUpdate[]>>? _orderHandler;
        private readonly Action<DataEvent<ToobitPositionUpdate[]>>? _positionHandler;
        private readonly Action<DataEvent<ToobitUserTradeUpdate[]>>? _userTradeHandler;

        /// <summary>
        /// ctor
        /// </summary>
        public ToobitFuturesUserDataSubscription(
            ILogger logger,
            ToobitSocketClientUsdtFuturesApi client,
            Action<DataEvent<ToobitAccountUpdate>>? accountHandler = null,
            Action<DataEvent<ToobitFuturesOrderUpdate[]>>? orderHandler = null,
            Action<DataEvent<ToobitPositionUpdate[]>>? positionHandler = null,
            Action<DataEvent<ToobitUserTradeUpdate[]>>? tradeHandler = null) : base(logger, false)
        {
            _client = client;
            _accountHandler = accountHandler;
            _orderHandler = orderHandler;
            _positionHandler = positionHandler;
            _userTradeHandler = tradeHandler;

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
            DateTime? timestamp = message.Length > 0 ? message.Max(x => x.EventTime) : null;
            if (timestamp != null)
                _client.UpdateTimeOffset(timestamp.Value);

            _accountHandler?.Invoke(
                    new DataEvent<ToobitAccountUpdate>(ToobitExchange.ExchangeName, message.First(), receiveTime, originalData)
                        .WithStreamId("FuturesAccount")
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(timestamp, _client.GetTimeOffset())
                );
            
            return CallResult.SuccessResult;
        }

        public CallResult HandleOrderUpdate(SocketConnection connection, DateTime receiveTime, string? originalData, ToobitFuturesOrderUpdate[] message)
        {
            DateTime? timestamp = message.Length > 0 ? message.Max(x => x.EventTime) : null;
            if (timestamp != null)
                _client.UpdateTimeOffset(timestamp.Value);

            _orderHandler?.Invoke(
                    new DataEvent<ToobitFuturesOrderUpdate[]>(ToobitExchange.ExchangeName, message, receiveTime, originalData)
                        .WithStreamId("FuturesOrder")
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(timestamp, _client.GetTimeOffset())
                );
            
            return CallResult.SuccessResult;
        }

        public CallResult HandleUserTradeUpdate(SocketConnection connection, DateTime receiveTime, string? originalData, ToobitUserTradeUpdate[] message)
        {
            DateTime? timestamp = message.Length > 0 ? message.Max(x => x.EventTime) : null;
            if (timestamp != null)
                _client.UpdateTimeOffset(timestamp.Value);

            _userTradeHandler?.Invoke(
                    new DataEvent<ToobitUserTradeUpdate[]>(ToobitExchange.ExchangeName, message, receiveTime, originalData)
                        .WithStreamId("UserTrade")
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(timestamp, _client.GetTimeOffset())
                );

            return CallResult.SuccessResult;
        }

        public CallResult HandlePositionUpdate(SocketConnection connection, DateTime receiveTime, string? originalData, ToobitPositionUpdate[] message)
        {
            DateTime? timestamp = message.Length > 0 ? message.Max(x => x.EventTime) : null;
            if (timestamp != null)
                _client.UpdateTimeOffset(timestamp.Value);

            _positionHandler?.Invoke(
                    new DataEvent<ToobitPositionUpdate[]>(ToobitExchange.ExchangeName, message, receiveTime, originalData)
                        .WithStreamId("FuturesPosition")
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(timestamp, _client.GetTimeOffset())
                );

            return CallResult.SuccessResult;
        }
    }
}
