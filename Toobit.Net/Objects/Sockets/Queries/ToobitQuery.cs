using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using System;

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

            // The wire error must complete this query's result type, rather than return CallResult<SocketError>.
            MessageRouter = MessageRouter.CreateForQuery<SocketError, T>("-100010", HandleSymbolError);
        }

        private CallResult<T> HandleSymbolError(SocketConnection connection, DateTime receiveTime, string? originalData, SocketError @event)
        {
            return CallResult.Fail<T>(new ServerError(@event.Code, _client.GetErrorInfo(@event.Code, @event.Description)));
        }
    }
}
