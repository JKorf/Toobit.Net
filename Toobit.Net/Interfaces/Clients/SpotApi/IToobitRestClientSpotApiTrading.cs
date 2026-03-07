using System.Threading.Tasks;
using System.Threading;
using Toobit.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using Toobit.Net.Enums;
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#test-new-order-trade" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/spot/orderTest
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name, for example 'ETHUSDT'</param>
        /// <param name="orderSide">["<c>side</c>"] Order side</param>
        /// <param name="orderType">["<c>type</c>"] Order type</param>
        /// <param name="quantity">["<c>quantity</c>"] Quantity of the order</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] Time in force</param>
        /// <param name="price">["<c>price</c>"] Limit order price</param>
        /// <param name="clientOrderId">["<c>newClientOrderId</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> PlaceTestOrderAsync(string symbol, OrderSide orderSide, OrderType orderType, decimal quantity, TimeInForce? timeInForce = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#new-order-trade" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/spot/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name, for example 'ETHUSDT'</param>
        /// <param name="orderSide">["<c>side</c>"] Order side</param>
        /// <param name="orderType">["<c>type</c>"] Order type</param>
        /// <param name="quantity">["<c>quantity</c>"] Quantity of the order. For market buy orders this is in quote asset.</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] Time in force</param>
        /// <param name="price">["<c>price</c>"] Limit order price</param>
        /// <param name="clientOrderId">["<c>newClientOrderId</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitOrder>> PlaceOrderAsync(string symbol, OrderSide orderSide, OrderType orderType, decimal quantity, TimeInForce? timeInForce = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Place multiple orders. Orders have to be for the same symbol.
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#place-multiple-orders-trade" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/spot/batchOrders
        /// </para>
        /// </summary>
        /// <param name="orders">Orders to place</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CallResult<ToobitOrder>[]>> PlaceMultipleOrdersAsync(IEnumerable<ToobitOrderRequest> orders, CancellationToken ct = default);

        /// <summary>
        /// Cancel an open order
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#cancel-order-trade" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v1/spot/order
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Id of order to cancel. Either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] Client order id of order to cancel. Either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitOrder>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders matching the parameters
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#cancel-all-open-orders-trade" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v1/spot/openOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] Filter by order side</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllOrdersAsync(string? symbol = null, OrderSide? side = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders by id. If successful no result is returned in the data, if unsuccessful an error is returned in the data
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#cancel-multiple-orders-trade" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v1/spot/cancelOrderByIds
        /// </para>
        /// </summary>
        /// <param name="orderIds">["<c>ids</c>"] Ids of the orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitCancelResult[]>> CancelMultipleOrdersAsync(IEnumerable<long> orderIds, CancellationToken ct = default);

        /// <summary>
        /// Get order info
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#query-order-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/spot/order
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Id of order to cancel, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>origClientOrderId</c>"] Client order id of the order to cancel, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitOrder>> GetOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#current-open-orders-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/spot/openOrders
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Filter by order id</param>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitOrder[]>> GetOpenOrdersAsync(long? orderId = null, string? symbol = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#all-orders-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/spot/tradeOrders
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Filter by order id</param>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitOrder[]>> GetOrdersAsync(long? orderId = null, string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#account-trade-list-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/account/trades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="fromId">["<c>fromId</c>"] Filter by min id</param>
        /// <param name="toId">["<c>toId</c>"] Filter by max id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitUserTrade[]>> GetUserTradesAsync(string symbol, long? fromId = null, long? toId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

    }
}
