using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Toobit.Net.Enums;
using Toobit.Net.Objects.Models;

namespace Toobit.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Toobit Spot exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IToobitRestClientSpotApiExchangeData
    {
        /// <summary>
        /// Get server time
        /// <para>
        /// Docs:<br />
        /// <a href="https://api-docs.toobit.com/api/spot-market-data#test-connectivity" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/time
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get exchange info
        /// <para>
        /// Docs:<br />
        /// <a href="https://api-docs.toobit.com/api/spot-market-data#exchange-information" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/exchangeInfo
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<ToobitExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the current order book
        /// <para>
        /// Docs:<br />
        /// <a href="https://api-docs.toobit.com/api/spot-market-data#order-book" /><br />
        /// Endpoint:<br />
        /// GET /quote/v1/depth
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 200</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<ToobitOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get the most recent trades for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://api-docs.toobit.com/api/spot-market-data#recent-trades-list" /><br />
        /// Endpoint:<br />
        /// GET /quote/v1/trades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 60</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<ToobitTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get klines
        /// <para>
        /// Docs:<br />
        /// <a href="https://api-docs.toobit.com/api/spot-market-data#kline-candlestick-data" /><br />
        /// Endpoint:<br />
        /// GET /quote/v1/klines
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<ToobitKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get 24h price info
        /// <para>
        /// Docs:<br />
        /// <a href="https://api-docs.toobit.com/api/spot-market-data#24hr-ticker-price-change-statistics" /><br />
        /// Endpoint:<br />
        /// GET /quote/v1/ticker/24hr
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<ToobitTicker[]>> GetTickersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get price info
        /// <para>
        /// Docs:<br />
        /// <a href="https://api-docs.toobit.com/api/spot-market-data#symbol-price-ticker" /><br />
        /// Endpoint:<br />
        /// GET /quote/v1/ticker/price
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<ToobitPrice[]>> GetPricesAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get best book prices info
        /// <para>
        /// Docs:<br />
        /// <a href="https://api-docs.toobit.com/api/spot-market-data#symbol-order-book-ticker" /><br />
        /// Endpoint:<br />
        /// GET /quote/v1/ticker/bookTicker
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<ToobitBookTicker[]>> GetBookTickersAsync(string? symbol = null, CancellationToken ct = default);

    }
}
