using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Toobit.Net.Enums;
using Toobit.Net.Interfaces.Clients.SpotApi;
using Toobit.Net.Objects.Models;

namespace Toobit.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class ToobitRestClientSpotApiExchangeData : IToobitRestClientSpotApiExchangeData
    {
        private readonly ToobitRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal ToobitRestClientSpotApiExchangeData(ILogger logger, ToobitRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Server Time

        /// <inheritdoc />
        public async Task<HttpResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/time", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitServerTime>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<DateTime>(result);

            return HttpResult.Ok(result, result.Data.Timestamp);
        }

        #endregion

        #region Get Exchange Info

        /// <inheritdoc />
        public async Task<HttpResult<ToobitExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/exchangeInfo", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitExchangeInfo>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<HttpResult<ToobitOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/quote/v1/depth", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitOrderBook>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public async Task<HttpResult<ToobitTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/quote/v1/trades", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<HttpResult<ToobitKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("interval", interval);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/quote/v1/klines", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<HttpResult<ToobitTicker[]>> GetTickersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/quote/v1/ticker/24hr", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Price

        /// <inheritdoc />
        public async Task<HttpResult<ToobitPrice[]>> GetPricesAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/quote/v1/ticker/price", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitPrice[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Book Tickers

        /// <inheritdoc />
        public async Task<HttpResult<ToobitBookTicker[]>> GetBookTickersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(ToobitExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/quote/v1/ticker/bookTicker", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitBookTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
