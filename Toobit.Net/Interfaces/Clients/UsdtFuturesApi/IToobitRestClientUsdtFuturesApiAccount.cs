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
        /// <a href="https://api-docs.toobit.com/api/usdt-m-account-and-trading#change-margin-type-trade" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/marginType
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="marginType">["<c>marginType</c>"] Type of margin</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<ToobitMarginType>> SetMarginTypeAsync(string symbol, MarginType marginType, CancellationToken ct = default);

        /// <summary>
        /// Change the initial leverage for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://api-docs.toobit.com/api/usdt-m-account-and-trading#change-initial-leverage-trade" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/leverage
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="leverage">["<c>leverage</c>"] New leverage value</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<ToobitLeverage>> SetLeverageAsync(string symbol, int leverage, CancellationToken ct = default);

        /// <summary>
        /// Get leverage info
        /// <para>
        /// Docs:<br />
        /// <a href="https://api-docs.toobit.com/api/usdt-m-account-and-trading#get-the-leverage-multiple-and-position-mode-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/accountLeverage
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<ToobitLeverageInfo>> GetLeverageInfoAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get futures balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://api-docs.toobit.com/api/usdt-m-account-and-trading#futures-account-balance-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/balance
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<ToobitFuturesBalance[]>> GetBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Adjust the position margin
        /// <para>
        /// Docs:<br />
        /// <a href="https://api-docs.toobit.com/api/usdt-m-account-and-trading#modify-isolated-position-margin-trade" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/futures/positionMargin
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="positionSide">["<c>side</c>"] Position side</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity of margin to adjust, positive to add, negative to remove</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<ToobitPositionMargin>> SetPositionMarginAsync(string symbol, PositionSide positionSide, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get transaction history
        /// <para>
        /// Docs:<br />
        /// <a href="https://api-docs.toobit.com/api/usdt-m-account-and-trading#get-futures-account-transaction-history-list-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/balanceFlow
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>symbol</c>"] The asset, for example `ETH`</param>
        /// <param name="flowType">["<c>flowType</c>"] Flow type</param>
        /// <param name="fromId">["<c>fromId</c>"] Return results after this id</param>
        /// <param name="endId">["<c>endId</c>"] Return results before this id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<ToobitFuturesTransaction[]>> GetTransactionHistoryAsync(string? asset = null, FlowType? flowType = null, long? fromId = null, long? endId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get fee rates
        /// <para>
        /// Docs:<br />
        /// <a href="https://api-docs.toobit.com/api/usdt-m-account-and-trading#user-commission-rate-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/futures/commissionRate
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-SWAP-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<ToobitFeeRates>> GetFeesAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Starts a user stream by requesting a listen key. This listen key can be used in subsequent requests to SubscribeToUserDataUpdates. The stream will close after 60 minutes unless a keep alive is send.
        /// <para>Note that the listenkey is shared between the Spot and UsdtFutures API</para>
        /// <para>
        /// Docs:<br />
        /// <a href="https://api-docs.toobit.com/api/usdt-m-websocket-account.html#user-data-streams" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/userDataStream
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<string>> StartUserStreamAsync(CancellationToken ct = default);

        /// <summary>
        /// Sends a keep alive for the current user stream listen key to keep the stream from closing. Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// <para>
        /// Docs:<br />
        /// <a href="https://api-docs.toobit.com/api/usdt-m-websocket-account.html#user-data-streams" /><br />
        /// Endpoint:<br />
        /// PUT /api/v1/userDataStream
        /// </para>
        /// </summary>
        /// <param name="listenKey">["<c>listenKey</c>"] The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Stops the current user stream
        /// <para>
        /// Docs:<br />
        /// <a href="https://api-docs.toobit.com/api/usdt-m-websocket-account.html#user-data-streams" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v1/userDataStream
        /// </para>
        /// </summary>
        /// <param name="listenKey">["<c>listenKey</c>"] The listen key to stop</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default);
    }
}
