using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects.Sockets;
using Toobit.Net.Objects.Models;
using System.Collections.Generic;
using Toobit.Net.Enums;
using CryptoExchange.Net.Interfaces.Clients;

namespace Toobit.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Toobit Spot streams
    /// </summary>
    public interface IToobitSocketClientSpotApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Subscribe to live trade updates
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#trade-streams" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<ToobitTradeUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to live trade updates
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#trade-streams" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<ToobitTradeUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to live price ticker updates
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#individual-symbol-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<ToobitTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to live price ticker updates
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#individual-symbol-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<ToobitTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to live kline/candlestick updates
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<ToobitKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to live kline/candlestick updates
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<ToobitKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to live price ticker updates
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#partial-book-depth-streams" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, Action<DataEvent<ToobitOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to live price ticker updates
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#partial-book-depth-streams" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<ToobitOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to live price ticker updates
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#diff-depth-stream" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<ToobitOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to live price ticker updates
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#diff-depth-stream" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<ToobitOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user data updates
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#user-data-streams" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the <see cref="IToobitRestClientSpotApiAccount.StartUserStreamAsync(CancellationToken)">StartUserStreamAsync</see> method</param>
        /// <param name="onAccountMessage">Event handler for account and balance updates</param>
        /// <param name="onOrderMessage">Event handler for order updates</param>
        /// <param name="onUserTradeMessage">Event handler for user trade updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<DataEvent<ToobitAccountUpdate>>? onAccountMessage = null,
            Action<DataEvent<ToobitOrderUpdate[]>>? onOrderMessage = null,
            Action<DataEvent<ToobitUserTradeUpdate[]>>? onUserTradeMessage = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get the shared socket requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IToobitSocketClientSpotApiShared SharedClient { get; }
    }
}
