using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Microsoft.Extensions.Logging;
using System;

namespace Toobit.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class ToobitPingSubscription : SystemSubscription
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ToobitPingSubscription(ILogger logger) : base(logger, false)
        {
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<PingRequest>("ping", DoHandleMessage);
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, PingRequest message)
        {
            _ = connection.SendAsync(ExchangeHelpers.NextId(), new PingResponse { Pong = message.Ping }, 1);
            return CallResult.SuccessResult;
        }
    }
}
