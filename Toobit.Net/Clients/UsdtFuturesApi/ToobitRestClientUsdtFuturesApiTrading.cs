using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;
using Toobit.Net.Enums;
using Toobit.Net.Objects.Models;
using Toobit.Net.Objects.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects.Errors;

namespace Toobit.Net.Clients.UsdtFuturesApi
{
    /// <inheritdoc />
    internal class ToobitRestClientUsdtFuturesApiTrading : IToobitRestClientUsdtFuturesApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly ToobitRestClientUsdtFuturesApi _baseClient;
        private readonly ILogger _logger;

        internal ToobitRestClientUsdtFuturesApiTrading(ILogger logger, ToobitRestClientUsdtFuturesApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }

        #region Place Order

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitFuturesOrder>> PlaceOrderAsync(string symbol, FuturesOrderSide orderSide, FuturesNewOrderType orderType, long quantity, decimal? price = null, PriceType? priceType = null, decimal? stopPrice = null, TimeInForce? timeInForce = null, string? clientOrderId = null, decimal? takeProfit = null, TriggerType? takeProfitTriggerType = null, decimal? takeProfitLimitPrice = null, OrderType? takeProfitOrderType = null, decimal? stopLoss = null, TriggerType? stopLossTriggerType = null, decimal? stopLossLimitPrice = null, OrderType? stopLossOrderType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("side", orderSide);
            parameters.AddEnum("type", orderType);
            parameters.Add("quantity", quantity);
            parameters.Add("newClientOrderId", clientOrderId ?? ExchangeHelpers.RandomString(24));
            parameters.AddOptional("price", price);
            parameters.AddOptionalEnum("priceType", priceType);
            parameters.AddOptional("stopPrice", stopPrice);
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptional("takeProfit", takeProfit);
            parameters.AddOptionalEnum("tpTriggerBy", takeProfitTriggerType);
            parameters.AddOptional("tpLimitPrice", takeProfitLimitPrice);
            parameters.AddOptionalEnum("tpOrderType", takeProfitOrderType);
            parameters.AddOptional("stopLoss", stopLoss);
            parameters.AddOptionalEnum("slTriggerBy", stopLossTriggerType);
            parameters.AddOptional("slLimitPrice", stopLossLimitPrice);
            parameters.AddOptionalEnum("slOrderType", stopLossOrderType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/futures/order", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<CallResult<ToobitFuturesOrder>[]>> PlaceMultipleOrdersAsync(IEnumerable<ToobitFuturesOrderRequest> orders, CancellationToken ct = default)
        {
            foreach (var order in orders.Where(x => x.ClientOrderId == null))
                order.ClientOrderId = ExchangeHelpers.RandomString(24);

            var parameters = new ParameterCollection();
            parameters.SetBody(orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/futures/batchOrders", ToobitExchange.RateLimiter.Toobit, 2, true, requestBodyFormat: RequestBodyFormat.Json);
            var resultData = await _baseClient.SendAsync<ToobitDataResult<ToobitFuturesOrderResult[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!resultData)
                return resultData.As<CallResult<ToobitFuturesOrder>[]>(default);

            var result = new List<CallResult<ToobitFuturesOrder>>();
            foreach (var item in resultData.Data.Result)
            {
                if (item.Order != null)
                    result.Add(new CallResult<ToobitFuturesOrder>(item.Order));
                else
                    result.Add(new CallResult<ToobitFuturesOrder>(new ServerError(item.Code, _baseClient.GetErrorInfo(item.Code, item.Message!))));
            }

            if (result.All(x => !x.Success))
                return resultData.AsErrorWithData(new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, "All orders failed")), result.ToArray());

            return resultData.As(result.ToArray());
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitFuturesOrder>> GetOrderAsync(long? orderId = null, string? clientOrderId = null, FuturesOrderType? orderType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("origClientOrderId", clientOrderId);
            parameters.AddOptionalEnum("type", orderType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/futures/order", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitFuturesOrder>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, FuturesOrderType? orderType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("origClientOrderId", clientOrderId);
            parameters.AddOptionalEnum("type", orderType);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v1/futures/order", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllOrdersAsync(string symbol, OrderSide side, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("side", side);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v1/futures/batchOrders", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.Code != 200)
                return result.AsDatalessError(new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return result.AsDataless();
        }

        #endregion

        #region Cancel Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitCancelResult[]>> CancelMultipleOrdersAsync(IEnumerable<long> orderIds, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("ids", string.Join(",", orderIds));
            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v1/futures/cancelOrderByIds", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitDataResult<ToobitCancelResult[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.As<ToobitCancelResult[]>(default);

            if (result.Data.Code != 0)
                return result.AsError<ToobitCancelResult[]>(new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, "Failed")));

            return result.As<ToobitCancelResult[]>(result.Data.Result.ToArray());
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitFuturesOrder[]>> GetOpenOrdersAsync(string? symbol = null, long? orderId = null, FuturesOrderType? orderType = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptionalEnum("type", orderType);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/futures/openOrders", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitFuturesOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Positions

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitPosition[]>> GetPositionsAsync(string? symbol = null, PositionSide? positionSide = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("side", positionSide);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/futures/positions", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitPosition[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Trading Stop

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitTradingStop>> SetTradingStopAsync(string symbol, PositionSide positionSide, decimal? takeProfitPrice = null, decimal? stopLossPrice = null, TriggerType? takeProfitTriggerType = null, TriggerType? StopLossTriggerType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("side", positionSide);
            parameters.AddOptionalString("takeProfit", takeProfitPrice);
            parameters.AddOptionalString("stopLoss", stopLossPrice);
            parameters.AddOptionalEnum("tpTriggerBy", takeProfitTriggerType);
            parameters.AddOptionalEnum("slTriggerBy", StopLossTriggerType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/futures/position/trading-stop", ToobitExchange.RateLimiter.Toobit, 3, true);
            var result = await _baseClient.SendAsync<ToobitTradingStop>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order History

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitFuturesOrder[]>> GetOrderHistoryAsync(string? symbol = null, long? toId = null, FuturesOrderType? orderType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptional("orderId", toId);
            parameters.AddOptionalEnum("type", orderType);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/futures/historyOrders", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitFuturesOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitFuturesUserTrade[]>> GetUserTradesAsync(string symbol, long? fromId = null, long? toId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptional("fromId", fromId);
            parameters.AddOptional("toId", toId);
            parameters.AddOptionalMillisecondsString("", startTime);
            parameters.AddOptionalMillisecondsString("", endTime);
            parameters.AddOptional("", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/futures/userTrades", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitFuturesUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
