using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Toobit.Net.Enums;
using Toobit.Net.Objects.Models;

namespace Toobit.Net.Interfaces.Clients.UsdtFuturesApi
{
    /// <summary>
    /// Toobit UsdtFutures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IToobitRestClientUsdtFuturesApiExchangeData
    {
        /// <summary>
        /// Get server time
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#test-connectivity" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get exchange info
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#exchange-information" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the current order book
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#order-book" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="limit">Max number of results, max 200</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get the most recent trades for a symbol
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#recent-trades-list" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="limit">Max number of results, max 60</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get klines
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#kline-candlestick-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get mark price klines
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#kline-candlestick-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitMarkKline[]>> GetMarkPriceKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get index price klines
        /// <para><a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#kline-candlestick-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFuturesKline[]>> GetIndexPriceKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get 24h price info
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#24hr-ticker-price-change-statistics" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="interval">Stats interval</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitTicker[]>> GetTickersAsync(string? symbol = null, TickerInterval? interval = null, CancellationToken ct = default);

        /// <summary>
        /// Get price info
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#symbol-price-ticker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitPrice[]>> GetPricesAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get best book prices info
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#symbol-order-book-ticker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitBookTicker[]>> GetBookTickersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get index prices
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#symbol-index-price" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitIndexPrices>> GetIndexPricesAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get mark price
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#mark-price" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitMarkPrice>> GetMarkPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get funding rate
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#funding-rate" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFundingRate[]>> GetFundingRateAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history for a symbol
        /// <para><a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#get-funding-rate-history" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="fromId">Filter by from id</param>
        /// <param name="endId">Filter by end id</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFundingHistory[]>> GetFundingRateHistoryAsync(string symbol, long? fromId = null, long? endId = null, int? limit = null, CancellationToken ct = default);

    }
}
