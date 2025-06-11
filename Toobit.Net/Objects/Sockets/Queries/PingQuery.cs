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
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public PingQuery() : base(new PingRequest() { Ping = DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).Value }, false, 1)
        {
            RequestTimeout = TimeSpan.FromSeconds(5);
            ListenerIdentifiers = new HashSet<string> { "pong" };
        }
    }
}
