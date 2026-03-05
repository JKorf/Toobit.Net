using System;
using System.Threading.Tasks;
using System.Threading;
using Toobit.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using Toobit.Net.Enums;

namespace Toobit.Net.Interfaces.Clients.UsdtFuturesApi
{
    /// <summary>
    /// Toobit UsdtFutures account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IToobitRestClientUsdtFuturesApiAccount
    {
        /// <summary>
        /// Change the margin type for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#change-margin-type-trade" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/marginType
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="marginType">Type of margin</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitMarginType>> SetMarginTypeAsync(string symbol, MarginType marginType, CancellationToken ct = default);

        /// <summary>
        /// Change the initial leverage for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#change-initial-leverage-trade" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/leverage
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="leverage">New leverage value</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitLeverage>> SetLeverageAsync(string symbol, int leverage, CancellationToken ct = default);

        /// <summary>
        /// Get leverage info
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#get-the-leverage-multiple-and-position-mode-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/accountLeverage
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitLeverageInfo>> GetLeverageInfoAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get futures balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#futures-account-balance-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/balance
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFuturesBalance[]>> GetBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Adjust the position margin
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#modify-isolated-position-margin-trade" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/positionMargin
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="positionSide">Position side</param>
        /// <param name="quantity">Quantity of margin to adjust, positive to add, negative to remove</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitPositionMargin>> SetPositionMarginAsync(string symbol, PositionSide positionSide, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get transaction history
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#get-futures-account-transaction-history-list-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/balanceFlow
        /// </para>
        /// </summary>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="flowType">Flow type</param>
        /// <param name="fromId">Return results after this id</param>
        /// <param name="endId">Return results before this id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFuturesTransaction[]>> GetTransactionHistoryAsync(string? asset = null, FlowType? flowType = null, long? fromId = null, long? endId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get fee rates
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/usdt_swap/v1/en/#user-commission-rate-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/commissionRate
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitFeeRates>> GetFeesAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Starts a user stream by requesting a listen key. This listen key can be used in subsequent requests to SubscribeToUserDataUpdates. The stream will close after 60 minutes unless a keep alive is send.
        /// <para>Note that the listenkey is shared between the Spot and UsdtFutures API</para>
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#listen-key-spot" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/userDataStream
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default);

        /// <summary>
        /// Sends a keep alive for the current user stream listen key to keep the stream from closing. Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#listen-key-spot" /><br />
        /// Endpoint:<br />
        /// PUT /api/v1/userDataStream
        /// </para>
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Stops the current user stream
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#listen-key-spot" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v1/userDataStream
        /// </para>
        /// </summary>
        /// <param name="listenKey">The listen key to stop</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default);
    }
}
