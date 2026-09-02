using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Toobit.Net.Enums;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;
using Toobit.Net.Objects.Models;

namespace Toobit.Net.Clients.UsdtFuturesApi
{
    /// <inheritdoc />
    internal class ToobitRestClientUsdtFuturesApiAccount : IToobitRestClientUsdtFuturesApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly ToobitRestClientUsdtFuturesApi _baseClient;

        internal ToobitRestClientUsdtFuturesApiAccount(ToobitRestClientUsdtFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Set Margin Type

        /// <inheritdoc />
        public async Task<HttpResult<ToobitMarginType>> SetMarginTypeAsync(string symbol, MarginType marginType, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("marginType", marginType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/marginType", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitMarginType>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Leverage

        /// <inheritdoc />
        public async Task<HttpResult<ToobitLeverage>> SetLeverageAsync(string symbol, int leverage, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("leverage", leverage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/leverage", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitLeverage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Leverage Info

        /// <inheritdoc />
        public async Task<HttpResult<ToobitLeverageInfo>> GetLeverageInfoAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/accountLeverage", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitLeverageInfo[]>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<ToobitLeverageInfo>(result);

            return HttpResult.Ok(result, result.Data.First());
        }

        #endregion

        #region Get Balances

        /// <inheritdoc />
        public async Task<HttpResult<ToobitFuturesBalance[]>> GetBalancesAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/balance", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitFuturesBalance[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Position Margin

        /// <inheritdoc />
        public async Task<HttpResult<ToobitPositionMargin>> SetPositionMarginAsync(string symbol, PositionSide positionSide, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", positionSide);
            parameters.Add("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/futures/positionMargin", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitPositionMargin>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Transaction History

        /// <inheritdoc />
        public async Task<HttpResult<ToobitFuturesTransaction[]>> GetTransactionHistoryAsync(string? asset = null, FlowType? flowType = null, long? fromId = null, long? endId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", asset);
            parameters.Add("flowType", flowType);
            parameters.Add("fromId", fromId);
            parameters.Add("endId", endId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/balanceFlow", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitFuturesTransaction[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Fees

        /// <inheritdoc />
        public async Task<HttpResult<ToobitFeeRates>> GetFeesAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/futures/commissionRate", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitFeeRates>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Create a ListenKey 
        /// <inheritdoc />
        public async Task<HttpResult<string>> StartUserStreamAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v1/userDataStream", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitListenKey>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<string>(result);

            return HttpResult.Ok(result, result.Data.ListenKey!);
        }

        #endregion

        #region Ping/Keep-alive a ListenKey

        /// <inheritdoc />
        public async Task<HttpResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings)
            {
                { "listenKey", listenKey }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Put, _baseClient.BaseAddress, "api/v1/userDataStream", ToobitExchange.RateLimiter.Toobit, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Invalidate a ListenKey
        /// <inheritdoc />
        public async Task<HttpResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings)
            {
                { "listenKey", listenKey }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "api/v1/userDataStream", ToobitExchange.RateLimiter.Toobit, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

    }
}
