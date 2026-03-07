using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Objects;
using Toobit.Net.Objects.Models;
using Toobit.Net.Enums;
using System;
using System.Collections.Generic;

namespace Toobit.Net.Interfaces.Clients.UsdtFuturesApi
{
    /// <summary>
    /// Toobit UsdtFutures trading endpoints, placing and managing orders.
    /// </summary>
    public interface IToobitRestClientUsdtFuturesApiTrading
    {
        /// <summary>
        /// Place a new order
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#new-order-trade" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="orderSide">["<c>side</c>"] Futures order side</param>
        /// <param name="orderType">["<c>type</c>"] Order type</param>
        /// <param name="quantity">["<c>quantity</c>"] Quantity in contracts</param>
        /// <param name="price">["<c>price</c>"] Order price</param>
        /// <param name="priceType">["<c>priceType</c>"] Price type</param>
        /// <param name="stopPrice">["<c>stopPrice</c>"] Stop price</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] Time in force</param>
        /// <param name="clientOrderId">["<c>newClientOrderId</c>"] Client order id</param>
        /// <param name="takeProfit">["<c>takeProfit</c>"] Take profit price</param>
        /// <param name="takeProfitTriggerType">["<c>tpTriggerBy</c>"] Take profit trigger type</param>
        /// <param name="takeProfitLimitPrice">["<c>tpLimitPrice</c>"] Take profit limit price</param>
        /// <param name="takeProfitOrderType">["<c>tpOrderType</c>"] Take profit order type</param>
        /// <param name="stopLoss">["<c>stopLoss</c>"] Stop loss price</param>
        /// <param name="stopLossTriggerType">["<c>slTriggerBy</c>"] Stop loss trigger type</param>
        /// <param name="stopLossLimitPrice">["<c>slLimitPrice</c>"] Stop loss limit price</param>
        /// <param name="stopLossOrderType">["<c>slOrderType</c>"] Stop loss order type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFuturesOrder>> PlaceOrderAsync(string symbol, FuturesOrderSide orderSide, FuturesNewOrderType orderType, long quantity, decimal? price = null, PriceType? priceType = null, decimal? stopPrice = null, TimeInForce? timeInForce = null, string? clientOrderId = null, decimal? takeProfit = null, TriggerType? takeProfitTriggerType = null, decimal? takeProfitLimitPrice = null, OrderType? takeProfitOrderType = null, decimal? stopLoss = null, TriggerType? stopLossTriggerType = null, decimal? stopLossLimitPrice = null, OrderType? stopLossOrderType = null, CancellationToken ct = default);

        /// <summary>
        /// Place multiple new orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#place-multiple-orders-trade" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/batchOrders
        /// </para>
        /// </summary>
        /// <param name="orders">Orders to place</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CallResult<ToobitFuturesOrder>[]>> PlaceMultipleOrdersAsync(IEnumerable<ToobitFuturesOrderRequest> orders, CancellationToken ct = default);

        /// <summary>
        /// Get order info
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#query-order-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/order
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>origClientOrderId</c>"] Client order id, either this or orderId should be provided</param>
        /// <param name="orderType">["<c>type</c>"] Order type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFuturesOrder>> GetOrderAsync(long? orderId = null, string? clientOrderId = null, FuturesOrderType? orderType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#cancel-order-trade" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v1/futures/order
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>origClientOrderId</c>"] Client order id, either this or orderId should be provided</param>
        /// <param name="orderType">["<c>type</c>"] Order type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFuturesOrder>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, FuturesOrderType? orderType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders matching the parameters
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#cancel-orders-trade" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v1/futures/batchOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllOrdersAsync(string symbol, OrderSide side, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders by id. If successful no result is returned in the data, if unsuccessful an error is returned in the data
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#cancel-orders-trade" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v1/futures/cancelOrderByIds
        /// </para>
        /// </summary>
        /// <param name="orderIds">["<c>ids</c>"] Ids of orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitCancelResult[]>> CancelMultipleOrdersAsync(IEnumerable<long> orderIds, CancellationToken ct = default);

        /// <summary>
        /// Get open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#query-current-open-order-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/openOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Filter by order id</param>
        /// <param name="orderType">["<c>type</c>"] Filter by type of orders</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFuturesOrder[]>> GetOpenOrdersAsync(string? symbol = null, long? orderId = null, FuturesOrderType? orderType = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#query-position-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/positions
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="positionSide">["<c>side</c>"] Filter by position side</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitPosition[]>> GetPositionsAsync(string? symbol = null, PositionSide? positionSide = null, CancellationToken ct = default);

        /// <summary>
        /// Set take profit / stop loss
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#set-trading-stop" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/position/trading-stop
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="positionSide">["<c>side</c>"] Position side</param>
        /// <param name="takeProfitPrice">["<c>takeProfit</c>"] Take profit price</param>
        /// <param name="stopLossPrice">["<c>stopLoss</c>"] Stop loss price</param>
        /// <param name="takeProfitTriggerType">["<c>tpTriggerBy</c>"] Take profit trigger type</param>
        /// <param name="StopLossTriggerType">["<c>slTriggerBy</c>"] Stop loss trigger type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitTradingStop>> SetTradingStopAsync(string symbol, PositionSide positionSide, decimal? takeProfitPrice = null, decimal? stopLossPrice = null, TriggerType? takeProfitTriggerType = null, TriggerType? StopLossTriggerType = null, CancellationToken ct = default);

        /// <summary>
        /// Get order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#query-history-orders-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/historyOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="toId">["<c>orderId</c>"] Return orders before this id</param>
        /// <param name="orderType">["<c>type</c>"] Filter by order type</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFuturesOrder[]>> GetOrderHistoryAsync(string? symbol = null, long? toId = null, FuturesOrderType? orderType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#account-trade-list-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/userTrades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="fromId">["<c>fromId</c>"] Return results after this id</param>
        /// <param name="toId">["<c>toId</c>"] Return results before this id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFuturesUserTrade[]>> GetUserTradesAsync(string symbol, long? fromId = null, long? toId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

    }
}
