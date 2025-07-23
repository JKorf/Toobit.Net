using CryptoExchange.Net;
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
    internal class ToobitPingSubscription : SystemSubscription
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ToobitPingSubscription(ILogger logger) : base(logger, false)
        {
            MessageMatcher = MessageMatcher.Create<PingRequest>("ping", DoHandleMessage);
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<PingRequest> message)
        {
            var dataMessage = (PingRequest)message.Data;
            connection.Send(ExchangeHelpers.NextId(), new PingResponse { Pong = dataMessage.Ping }, 1);
            return CallResult.SuccessResult;
        }
    }
}
