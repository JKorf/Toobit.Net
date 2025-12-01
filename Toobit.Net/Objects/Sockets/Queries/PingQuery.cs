using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using Toobit.Net.Objects.Models;

namespace Toobit.Net.Objects.Sockets
{
    internal class PingQuery : Query<PingResponse>
    {
        public PingQuery() : base(new PingRequest() { Ping = DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).Value }, false, 1)
        {
            RequestTimeout = TimeSpan.FromSeconds(5);
            MessageMatcher = MessageMatcher.Create<PingResponse>("pong");
            MessageRouter = MessageRouter.CreateWithoutHandler<PingResponse>("pong");
        }
    }
}
