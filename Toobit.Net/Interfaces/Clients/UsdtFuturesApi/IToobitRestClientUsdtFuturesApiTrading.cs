using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Objects;
using Toobit.Net.Objects.Models;
using Toobit.Net.Enums;
using Toobit.Net.Objects.Internal;
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
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#new-order-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="orderSide">Futures order side</param>
        /// <param name="orderType">Order type</param>
        /// <param name="quantity">Quantity in contracts</param>
        /// <param name="price">Order price</param>
        /// <param name="priceType">Price type</param>
        /// <param name="stopPrice">Stop price</param>
        /// <param name="timeInForce">Time in force</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="takeProfit">Take profit price</param>
        /// <param name="takeProfitTriggerType">Take profit trigger type</param>
        /// <param name="takeProfitLimitPrice">Take profit limit price</param>
        /// <param name="takeProfitOrderType">Take profit order type</param>
        /// <param name="stopLoss">Stop loss price</param>
        /// <param name="stopLossTriggerType">Stop loss trigger type</param>
        /// <param name="stopLossLimitPrice">Stop loss limit price</param>
        /// <param name="stopLossOrderType">Stop loss order type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFuturesOrder>> PlaceOrderAsync(string symbol, FuturesOrderSide orderSide, FuturesNewOrderType orderType, long quantity, decimal? price = null, PriceType? priceType = null, decimal? stopPrice = null, TimeInForce? timeInForce = null, string? clientOrderId = null, decimal? takeProfit = null, TriggerType? takeProfitTriggerType = null, decimal? takeProfitLimitPrice = null, OrderType? takeProfitOrderType = null, decimal? stopLoss = null, TriggerType? stopLossTriggerType = null, decimal? stopLossLimitPrice = null, OrderType? stopLossOrderType = null, CancellationToken ct = default);

        /// <summary>
        /// Place multiple new orders
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#place-multiple-orders-trade" /></para>
        /// </summary>
        /// <param name="orders">Orders to place</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CallResult<ToobitFuturesOrder>[]>> PlaceMultipleOrdersAsync(ToobitFuturesOrderRequest[] orders, CancellationToken ct = default);

        /// <summary>
        /// Get order info
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#query-order-user_data" /></para>
        /// </summary>
        /// <param name="orderId">Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or orderId should be provided</param>
        /// <param name="orderType">Order type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFuturesOrder>> GetOrderAsync(long? orderId = null, string? clientOrderId = null, FuturesOrderType? orderType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#cancel-order-trade" /></para>
        /// </summary>
        /// <param name="orderId">Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or orderId should be provided</param>
        /// <param name="orderType">Order type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFuturesOrder>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, FuturesOrderType? orderType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders matching the parameters
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#cancel-orders-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="side">Order side</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllOrdersAsync(string symbol, OrderSide side, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders by id. If successful no result is returned in the data, if unsuccessful an error is returned in the data
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#cancel-orders-trade" /></para>
        /// </summary>
        /// <param name="orderIds">Ids of orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitCancelResult[]>> CancelMultipleOrdersAsync(IEnumerable<long> orderIds, CancellationToken ct = default);

        /// <summary>
        /// Get open orders
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#query-current-open-order-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="orderType">Filter by type of orders</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFuturesOrder[]>> GetOpenOrdersAsync(string? symbol = null, long? orderId = null, FuturesOrderType? orderType = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get positions
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#query-position-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="positionSide">Filter by position side</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitPosition[]>> GetPositionsAsync(string? symbol = null, PositionSide? positionSide = null, CancellationToken ct = default);

        /// <summary>
        /// Set take profit / stop loss
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#set-trading-stop" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="positionSide">Position side</param>
        /// <param name="takeProfitPrice">Take profit price</param>
        /// <param name="stopLossPrice">Stop loss price</param>
        /// <param name="takeProfitTriggerType">Take profit trigger type</param>
        /// <param name="StopLossTriggerType">Stop loss trigger type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitTradingStop>> SetTradingStopAsync(string symbol, PositionSide positionSide, decimal? takeProfitPrice = null, decimal? stopLossPrice = null, TriggerType? takeProfitTriggerType = null, TriggerType? StopLossTriggerType = null, CancellationToken ct = default);

        /// <summary>
        /// Get order history
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#query-history-orders-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="toId">Return orders before this id</param>
        /// <param name="orderType">Filter by order type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFuturesOrder[]>> GetOrderHistoryAsync(string? symbol = null, long? toId = null, FuturesOrderType? orderType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trades
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#account-trade-list-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="fromId">Return results after this id</param>
        /// <param name="toId">Return results before this id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFuturesUserTrade[]>> GetUserTradesAsync(string symbol, long? fromId = null, long? toId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

    }
}
