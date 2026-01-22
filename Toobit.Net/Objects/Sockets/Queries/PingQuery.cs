using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Sockets;
using System;

namespace Toobit.Net.Objects.Sockets
{
    internal class PingQuery : Query<PingResponse>
    {
        public PingQuery() : base(new PingRequest() { Ping = DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).Value }, false, 1)
        {
            RequestTimeout = TimeSpan.FromSeconds(5);
            MessageRouter = MessageRouter.CreateWithoutHandler<PingResponse>("pong");
        }
    }
}
