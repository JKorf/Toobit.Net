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
        public async Task<HttpResult<ToobitFuturesOrder>> PlaceOrderAsync(string symbol, FuturesOrderSide orderSide, FuturesNewOrderType orderType, long quantity, decimal? price = null, PriceType? priceType = null, decimal? stopPrice = null, TimeInForce? timeInForce = null, string? clientOrderId = null, decimal? takeProfit = null, TriggerType? takeProfitTriggerType = null, decimal? takeProfitLimitPrice = null, OrderType? takeProfitOrderType = null, decimal? stopLoss = null, TriggerType? stopLossTriggerType = null, decimal? stopLossLimitPrice = null, OrderType? stopLossOrderType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", orderSide);
            parameters.Add("type", orderType);
            parameters.Add("quantity", quantity);
            parameters.Add("newClientOrderId", clientOrderId ?? ExchangeHelpers.RandomString(24));
            parameters.Add("price", price);
            parameters.Add("priceType", priceType);
            parameters.Add("stopPrice", stopPrice);
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("takeProfit", takeProfit);
            parameters.Add("tpTriggerBy", takeProfitTriggerType);
            parameters.Add("tpLimitPrice", takeProfitLimitPrice);
            parameters.Add("tpOrderType", takeProfitOrderType);
            parameters.Add("stopLoss", stopLoss);
            parameters.Add("slTriggerBy", stopLossTriggerType);
            parameters.Add("slLimitPrice", stopLossLimitPrice);
            parameters.Add("slOrderType", stopLossOrderType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/order", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Multiple Orders

        /// <inheritdoc />
        public async Task<HttpResult<CallResult<ToobitFuturesOrder>[]>> PlaceMultipleOrdersAsync(IEnumerable<ToobitFuturesOrderRequest> orders, CancellationToken ct = default)
        {
            foreach (var order in orders.Where(x => x.ClientOrderId == null))
                order.ClientOrderId = ExchangeHelpers.RandomString(24);

            var parameters = new Parameters(orders.ToArray(), ToobitExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/batchOrders", ToobitExchange.RateLimiter.Toobit, 2, true, requestBodyFormat: RequestBodyFormat.Json);
            var resultData = await _baseClient.SendAsync<ToobitDataResult<ToobitFuturesOrderResult[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!resultData.Success)
                return HttpResult.Fail<CallResult<ToobitFuturesOrder>[]>(resultData);

            var result = new List<CallResult<ToobitFuturesOrder>>();
            foreach (var item in resultData.Data.Result)
            {
                if (item.Order != null)
                    result.Add(CallResult.Ok(item.Order));
                else
                    result.Add(CallResult.Fail<ToobitFuturesOrder>(new ServerError(item.Code, _baseClient.GetErrorInfo(item.Code, item.Message!))));
            }

            if (result.All(x => !x.Success))
                return HttpResult.Fail<CallResult<ToobitFuturesOrder>[]>(resultData, new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, "All orders failed")), result.ToArray());

            return HttpResult.Ok(resultData, result.ToArray());
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<ToobitFuturesOrder>> GetOrderAsync(long? orderId = null, string? clientOrderId = null, FuturesOrderType? orderType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            parameters.Add("origClientOrderId", clientOrderId);
            parameters.Add("type", orderType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/order", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<ToobitFuturesOrder>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, FuturesOrderType? orderType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            parameters.Add("origClientOrderId", clientOrderId);
            parameters.Add("type", orderType);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v1/futures/order", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<HttpResult> CancelAllOrdersAsync(string symbol, OrderSide side, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", side);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v1/futures/batchOrders", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 200)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result);
        }

        #endregion

        #region Cancel Multiple Orders

        /// <inheritdoc />
        public async Task<HttpResult<ToobitCancelResult[]>> CancelMultipleOrdersAsync(IEnumerable<long> orderIds, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("ids", string.Join(",", orderIds));
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v1/futures/cancelOrderByIds", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitDataResult<ToobitCancelResult[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<ToobitCancelResult[]>(result);

            if (result.Data.Code != 200)
                return HttpResult.Fail<ToobitCancelResult[]>(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, "Failed")));

            return HttpResult.Ok(result, result.Data.Result);
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<ToobitFuturesOrder[]>> GetOpenOrdersAsync(string? symbol = null, long? orderId = null, FuturesOrderType? orderType = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", orderId);
            parameters.Add("type", orderType);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/openOrders", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitFuturesOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Positions

        /// <inheritdoc />
        public async Task<HttpResult<ToobitPosition[]>> GetPositionsAsync(string? symbol = null, PositionSide? positionSide = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", positionSide);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/positions", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitPosition[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Trading Stop

        /// <inheritdoc />
        public async Task<HttpResult<ToobitTradingStop>> SetTradingStopAsync(string symbol, PositionSide positionSide, decimal? takeProfitPrice = null, decimal? stopLossPrice = null, TriggerType? takeProfitTriggerType = null, TriggerType? StopLossTriggerType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", positionSide);
            parameters.AddAsString("takeProfit", takeProfitPrice);
            parameters.AddAsString("stopLoss", stopLossPrice);
            parameters.Add("tpTriggerBy", takeProfitTriggerType);
            parameters.Add("slTriggerBy", StopLossTriggerType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/position/trading-stop", ToobitExchange.RateLimiter.Toobit, 3, true);
            var result = await _baseClient.SendAsync<ToobitTradingStop>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order History

        /// <inheritdoc />
        public async Task<HttpResult<ToobitFuturesOrder[]>> GetOrderHistoryAsync(string? symbol = null, long? toId = null, FuturesOrderType? orderType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", toId);
            parameters.Add("type", orderType);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/historyOrders", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitFuturesOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<ToobitFuturesUserTrade[]>> GetUserTradesAsync(string symbol, long? fromId = null, long? toId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("fromId", fromId);
            parameters.Add("toId", toId);
            parameters.Add("", startTime);
            parameters.Add("", endTime);
            parameters.Add("", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/userTrades", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitFuturesUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
