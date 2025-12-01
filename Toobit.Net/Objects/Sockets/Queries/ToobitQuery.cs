using CryptoExchange.Net.Clients;
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
        private readonly SocketApiClient _client;

        public ToobitQuery(SocketApiClient client, SocketRequest request, bool authenticated, TimeSpan waitForErrorTimeout) : base(request, authenticated, 1)
        {
            _client = client;

            // If there is no response that means it's successful. Wait for x seconds for an error message else assume success
            RequestTimeout = waitForErrorTimeout;
            TimeoutBehavior = TimeoutBehavior.Succeed;

            MessageMatcher = MessageMatcher.Create<SocketError>("-100010", HandleSymbolError);
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<SocketError>("-100010", HandleSymbolError);
        }

        private CallResult HandleSymbolError(SocketConnection connection, DateTime receiveTime, string? originalData, SocketError @event)
        {
            return new CallResult(new ServerError(@event.Code, _client.GetErrorInfo(@event.Code, @event.Description)));
        }
    }
}
