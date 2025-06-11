using System.Threading.Tasks;
using System.Threading;
using Toobit.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using Toobit.Net.Enums;
using System.Drawing;
using Toobit.Net.Objects.Internal;
using System;
using System.Collections.Generic;

namespace Toobit.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Toobit Spot trading endpoints, placing and managing orders.
    /// </summary>
    public interface IToobitRestClientSpotApiTrading
    {
        /// <summary>
        /// Place a test order, only validates the request, doesn't place the order
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#test-new-order-trade" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name, for example 'ETHUSDT'</param>
        /// <param name="orderSide">Order side</param>
        /// <param name="orderType">Order type</param>
        /// <param name="quantity">Quantity of the order</param>
        /// <param name="timeInForce">Time in force</param>
        /// <param name="price">Limit order price</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> PlaceTestOrderAsync(string symbol, OrderSide orderSide, OrderType orderType, decimal quantity, TimeInForce? timeInForce = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#new-order-trade" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name, for example 'ETHUSDT'</param>
        /// <param name="orderSide">Order side</param>
        /// <param name="orderType">Order type</param>
        /// <param name="quantity">Quantity of the order. For market buy orders this is in quote asset.</param>
        /// <param name="timeInForce">Time in force</param>
        /// <param name="price">Limit order price</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitOrder>> PlaceOrderAsync(string symbol, OrderSide orderSide, OrderType orderType, decimal quantity, TimeInForce? timeInForce = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Place multiple orders. Orders have to be for the same symbol.
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#place-multiple-orders-trade" /></para>
        /// </summary>
        /// <param name="orders">Orders to place</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CallResult<ToobitOrder>[]>> PlaceMultipleOrdersAsync(ToobitOrderRequest[] orders, CancellationToken ct = default);

        /// <summary>
        /// Cancel an open order
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#cancel-order-trade" /></para>
        /// </summary>
        /// <param name="orderId">Id of order to cancel. Either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id of order to cancel. Either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitOrder>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders matching the parameters
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#cancel-all-open-orders-trade" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="side">Filter by order side</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllOrdersAsync(string? symbol = null, OrderSide? side = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders by id. If successful no result is returned in the data, if unsuccessful an error is returned in the data
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#cancel-multiple-orders-trade" /></para>
        /// </summary>
        /// <param name="orderIds">Ids of the orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitCancelResult[]>> CancelMultipleOrdersAsync(IEnumerable<long> orderIds, CancellationToken ct = default);

        /// <summary>
        /// Get order info
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#query-order-user_data" /></para>
        /// </summary>
        /// <param name="orderId">Id of order to cancel, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id of the order to cancel, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitOrder>> GetOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get open orders
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#current-open-orders-user_data" /></para>
        /// </summary>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitOrder[]>> GetOpenOrdersAsync(long? orderId = null, string? symbol = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get orders
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#all-orders-user_data" /></para>
        /// </summary>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitOrder[]>> GetOrdersAsync(long? orderId = null, string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trades
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#account-trade-list-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="fromId">Filter by min id</param>
        /// <param name="toId">Filter by max id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitUserTrade[]>> GetUserTradesAsync(string symbol, long? fromId = null, long? toId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

    }
}
