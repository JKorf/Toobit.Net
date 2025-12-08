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
        public async Task<WebCallResult> PlaceTestOrderAsync(string symbol, OrderSide orderSide, OrderType orderType, decimal quantity, TimeInForce? timeInForce = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("side", orderSide);
            parameters.AddEnum("type", orderType);
            parameters.Add("quantity", quantity);
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptional("price", price);
            parameters.AddOptional("newClientOrderId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/spot/orderTest", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Order

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitOrder>> PlaceOrderAsync(string symbol, OrderSide orderSide, OrderType orderType, decimal quantity, TimeInForce? timeInForce = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("side", orderSide);
            parameters.AddEnum("type", orderType);
            parameters.Add("quantity", quantity);
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptional("price", price);
            parameters.AddOptional("newClientOrderId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/spot/order", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<CallResult<ToobitOrder>[]>> PlaceMultipleOrdersAsync(IEnumerable<ToobitOrderRequest> orders, CancellationToken ct = default)
        {
            foreach (var order in orders.Where(x => string.IsNullOrEmpty(x.ClientOrderId)))
                order.ClientOrderId = ExchangeHelpers.RandomString(24);

            var parameters = new ParameterCollection();
            parameters.SetBody(orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/spot/batchOrders", ToobitExchange.RateLimiter.Toobit, 2, true, requestBodyFormat: RequestBodyFormat.Json);
            var resultData = await _baseClient.SendAsync<ToobitDataResult<ToobitOrderResult[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!resultData)
                return resultData.As<CallResult<ToobitOrder>[]>(default);

            var result = new List<CallResult<ToobitOrder>>();
            foreach (var item in resultData.Data.Result)
            {
                if (item.Order != null)
                    result.Add(new CallResult<ToobitOrder>(item.Order));
                else
                    result.Add(new CallResult<ToobitOrder>(new ServerError(item.Code, _baseClient.GetErrorInfo(item.Code, item.Message!))));
            }

            if (result.All(x => !x.Success))
                return resultData.AsErrorWithData(new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, "All orders failed")), result.ToArray());

            return resultData.As(result.ToArray());
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitOrder>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("clientOrderId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v1/spot/order", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllOrdersAsync(string? symbol = null, OrderSide? side = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("side", side);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v1/spot/openOrders", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitSuccess>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (!result.Data.Success)
                return result.AsDatalessError(new ServerError(new ErrorInfo(ErrorType.Unknown, "Failed")));

            return result.AsDataless();
        }

        #endregion

        #region Cancel Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitCancelResult[]>> CancelMultipleOrdersAsync(IEnumerable<long> orderIds, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("ids", string.Join(",", orderIds));
            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v1/spot/cancelOrderByIds", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitDataResult<ToobitCancelResult[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.As<ToobitCancelResult[]>(default);

            if (result.Data.Code != 0)
                return result.AsError<ToobitCancelResult[]>(new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, "Failed")));

            return result.As<ToobitCancelResult[]>(result.Data.Result.ToArray());
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitOrder>> GetOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("origClientOrderId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/spot/order", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitOrder[]>> GetOpenOrdersAsync(long? orderId = null, string? symbol = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/spot/openOrders", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitOrder[]>> GetOrdersAsync(long? orderId = null, string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/spot/tradeOrders", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitUserTrade[]>> GetUserTradesAsync(string symbol, long? fromId = null, long? toId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptional("fromId", fromId);
            parameters.AddOptional("toId", toId);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/account/trades", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
