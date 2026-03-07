using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Objects;
using Toobit.Net.Objects.Models;
using System;
using Toobit.Net.Enums;

namespace Toobit.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Toobit Spot account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IToobitRestClientSpotApiAccount
    {
        /// <summary>
        /// Get account balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#account-information-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/account
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitUserBalance>> GetBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Withdraw funds
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#withdraw-user_data" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/account/withdraw
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>coin</c>"] The asset, for example `ETH`</param>
        /// <param name="address">["<c>address</c>"] Address to withdraw to</param>
        /// <param name="quantity">["<c>quantity</c>"] Quantity to withdraw</param>
        /// <param name="network">["<c>chainType</c>"] Network to use</param>
        /// <param name="tag">["<c>addressExt</c>"] Tag</param>
        /// <param name="vaspCode">["<c>vaspCode</c>"] Vasp code</param>
        /// <param name="targetPersonFirstName">["<c>targetPersonFirstName</c>"] Target person first name</param>
        /// <param name="targetPersonLastName">["<c>targetPersonLastName</c>"] Target person last name</param>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitWithdrawResult>> WithdrawAsync(string asset, string address, decimal quantity, string? network = null, string? tag = null, string? vaspCode = null, string? targetPersonFirstName = null, string? targetPersonLastName = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal history
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#withdrawal-records-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/account/withdrawOrders
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>coin</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="fromId">["<c>fromId</c>"] Return results after this id</param>
        /// <param name="withdrawOrderId">["<c>withdrawOrderId</c>"] Filter by withdraw order id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitWithdrawal[]>> GetWithdrawalsAsync(string? asset = null, string? fromId = null, long? withdrawOrderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get deposit address
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#deposit-address-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/account/deposit/address
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>coin</c>"] The asset, for example `ETH`</param>
        /// <param name="network">["<c>chainType</c>"] The network</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitDepositAddress>> GetDepositAddressAsync(string asset, string network, CancellationToken ct = default);

        /// <summary>
        /// Get deposit history
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#deposit-history-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/account/depositOrders
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>coin</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="fromId">["<c>fromId</c>"] Return results after this id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitDeposit[]>> GetDepositsAsync(string? asset = null, string? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer asset. Can be between account types or between parent and sub account. If transferring between Spot and Futures for the current account the fromUid and toUid should be set to the current account id which can be retrieved with GetBalances
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#account-transfer" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/subAccount/transfer
        /// </para>
        /// </summary>
        /// <param name="fromUid">["<c>fromUid</c>"] From user id</param>
        /// <param name="toUid">["<c>toUid</c>"] To user id</param>
        /// <param name="fromAccountType">["<c>fromAccountType</c>"] From account type</param>
        /// <param name="toAccountType">["<c>toAccountType</c>"] To account type</param>
        /// <param name="asset">["<c>asset</c>"] The asset, for example `ETH`</param>
        /// <param name="quantity">["<c>quantity</c>"] Quantity</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> TransferAsync(long fromUid, long toUid, AccountType fromAccountType, AccountType toAccountType, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get transaction history
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#get-account-transaction-history-list-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/account/balanceFlow
        /// </para>
        /// </summary>
        /// <param name="accountType">["<c>accountType</c>"] Account type</param>
        /// <param name="asset">["<c>coin</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="flowType">["<c>flowType</c>"] Flow type</param>
        /// <param name="fromId">["<c>fromId</c>"] Return results after this id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitTransaction[]>> GetTransactionHistoryAsync(int? accountType = null, string? asset = null, int? flowType = null, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);
        
        /// <summary>
        /// Get sub account list
        /// <para>
        /// Docs:<br />
        /// <a href="https://toobit-docs.github.io/apidocs/spot/v1/en/#query-sub-account-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/account/checkApiKey
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<ToobitSubAccount[]>> GetSubAccountsAsync(CancellationToken ct = default);

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
        /// <param name="listenKey">["<c>listenKey</c>"] The listen key to keep alive</param>
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
        /// <param name="listenKey">["<c>listenKey</c>"] The listen key to stop</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default);
    }
}
