using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using Toobit.Net.Objects.Models;

namespace Toobit.Net.Objects.Sockets
{
    internal class ToobitQuery<T> : Query<T>
    {
        public ToobitQuery(SocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            ExpectsResponse = false;;

            MessageMatcher = MessageMatcher.Create([]);
        }
    }
}
