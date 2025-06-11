using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Toobit.Net.Enums;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;
using Toobit.Net.Objects.Internal;
using Toobit.Net.Objects.Models;

namespace Toobit.Net.Clients.UsdtFuturesApi
{
    /// <inheritdoc />
    internal class ToobitRestClientUsdtFuturesApiExchangeData : IToobitRestClientUsdtFuturesApiExchangeData
    {
        private readonly ToobitRestClientUsdtFuturesApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal ToobitRestClientUsdtFuturesApiExchangeData(ILogger logger, ToobitRestClientUsdtFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/time", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitServerTime>(request, null, ct).ConfigureAwait(false);
            return result.As<DateTime>(result.Data?.Timestamp ?? default);
        }

        #endregion

        #region Get Exchange Info

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/exchangeInfo", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitExchangeInfo>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptional("limit", limit);
            var weight = limit <= 100 ? 1 : limit <= 500 ? 5 : 10;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/quote/v1/depth", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitOrderBook>(request, parameters, ct, weight).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/quote/v1/trades", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/quote/v1/klines", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Mark Price Klines

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitMarkKline[]>> GetMarkPriceKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/quote/v1/markPrice/klines", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitWrapper<ToobitMarkKline[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.As<ToobitMarkKline[]>(default);

            return result.As(result.Data.Data);
        }

        #endregion

        #region Get Index Price Klines

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitFuturesKline[]>> GetIndexPriceKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/quote/v1/index/klines", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitWrapper<ToobitFuturesKline[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.As<ToobitFuturesKline[]>(default);

            return result.As(result.Data.Data);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitTicker[]>> GetTickersAsync(string? symbol = null, TickerInterval? interval = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("realtimeInterval", interval);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/quote/v1/contract/ticker/24hr", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Price

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitPrice[]>> GetPricesAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/quote/v1/ticker/price", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitPrice[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Book Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitBookTicker[]>> GetBookTickersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/quote/v1/ticker/bookTicker", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitBookTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Index Prices

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitIndexPrices>> GetIndexPricesAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/quote/v1/index", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitIndexPrices>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Mark Price

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitMarkPrice>> GetMarkPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/quote/v1/markPrice", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitMarkPrice>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Rate

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitFundingRate[]>> GetFundingRateAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/futures/fundingRate", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitFundingRate[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitFundingHistory[]>> GetFundingRateHistoryAsync(string symbol, long? fromId = null, long? endId = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptional("fromId", fromId);
            parameters.AddOptional("endId", endId);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/futures/historyFundingRate", ToobitExchange.RateLimiter.Toobit, 1, false);
            var result = await _baseClient.SendAsync<ToobitFundingHistory[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
