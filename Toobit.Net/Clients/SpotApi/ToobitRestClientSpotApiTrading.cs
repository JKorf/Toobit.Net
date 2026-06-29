using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Toobit.Net.Interfaces.Clients.SpotApi;
using Toobit.Net.Objects.Models;
using Toobit.Net.Enums;
using System;
using Toobit.Net.Objects.Internal;
using System.Collections.Generic;
using System.Linq;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects.Errors;

namespace Toobit.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class ToobitRestClientSpotApiTrading : IToobitRestClientSpotApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly ToobitRestClientSpotApi _baseClient;
        private readonly ILogger _logger;

        internal ToobitRestClientSpotApiTrading(ILogger logger, ToobitRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }

        #region Place Test Order

        /// <inheritdoc />
        public async Task<HttpResult> PlaceTestOrderAsync(string symbol, OrderSide orderSide, OrderType orderType, decimal quantity, TimeInForce? timeInForce = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", orderSide);
            parameters.Add("type", orderType);
            parameters.Add("quantity", quantity);
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("price", price);
            parameters.Add("newClientOrderId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/spot/orderTest", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Order

        /// <inheritdoc />
        public async Task<HttpResult<ToobitOrder>> PlaceOrderAsync(string symbol, OrderSide orderSide, OrderType orderType, decimal quantity, TimeInForce? timeInForce = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", orderSide);
            parameters.Add("type", orderType);
            parameters.Add("quantity", quantity);
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("price", price);
            parameters.Add("newClientOrderId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/spot/order", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Multiple Orders

        /// <inheritdoc />
        public async Task<HttpResult<CallResult<ToobitOrder>[]>> PlaceMultipleOrdersAsync(IEnumerable<ToobitOrderRequest> orders, CancellationToken ct = default)
        {
            foreach (var order in orders.Where(x => string.IsNullOrEmpty(x.ClientOrderId)))
                order.ClientOrderId = ExchangeHelpers.RandomString(24);

            var parameters = new Parameters(orders.ToArray(), ToobitExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/spot/batchOrders", ToobitExchange.RateLimiter.Toobit, 2, true, requestBodyFormat: RequestBodyFormat.Json);
            var resultData = await _baseClient.SendAsync<ToobitDataResult<ToobitOrderResult[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!resultData.Success)
                return HttpResult.Fail<CallResult<ToobitOrder>[]>(resultData);

            var result = new List<CallResult<ToobitOrder>>();
            foreach (var item in resultData.Data.Result)
            {
                if (item.Order != null)
                    result.Add(CallResult.Ok(item.Order));
                else
                    result.Add(CallResult.Fail<ToobitOrder>(new ServerError(item.Code, _baseClient.GetErrorInfo(item.Code, item.Message!))));
            }

            if (result.All(x => !x.Success))
                return HttpResult.Fail<CallResult<ToobitOrder>[]>(resultData, new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, "All orders failed")), result.ToArray());

            return HttpResult.Ok(resultData, result.ToArray());
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<ToobitOrder>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            parameters.Add("clientOrderId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v1/spot/order", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<HttpResult> CancelAllOrdersAsync(string? symbol = null, OrderSide? side = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", side);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v1/spot/openOrders", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitSuccess>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (!result.Data.Success)
                return HttpResult.Fail(result, new ServerError(new ErrorInfo(ErrorType.Unknown, "Failed")));

            return HttpResult.Ok(result);
        }

        #endregion

        #region Cancel Multiple Orders

        /// <inheritdoc />
        public async Task<HttpResult<ToobitCancelResult[]>> CancelMultipleOrdersAsync(IEnumerable<long> orderIds, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("ids", string.Join(",", orderIds));
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v1/spot/cancelOrderByIds", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitDataResult<ToobitCancelResult[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<ToobitCancelResult[]>(result);

            if (result.Data.Code != 0)
                return HttpResult.Fail<ToobitCancelResult[]>(result, new ServerError(new ErrorInfo(ErrorType.Unknown, "Failed")));

            return HttpResult.Ok(result, result.Data.Result.ToArray());
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<ToobitOrder>> GetOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            parameters.Add("origClientOrderId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/spot/order", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<ToobitOrder[]>> GetOpenOrdersAsync(long? orderId = null, string? symbol = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            parameters.Add("symbol", symbol);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/spot/openOrders", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<HttpResult<ToobitOrder[]>> GetOrdersAsync(long? orderId = null, string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            parameters.Add("symbol", symbol);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/spot/tradeOrders", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<ToobitUserTrade[]>> GetUserTradesAsync(string symbol, long? fromId = null, long? toId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("fromId", fromId);
            parameters.Add("toId", toId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/account/trades", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
