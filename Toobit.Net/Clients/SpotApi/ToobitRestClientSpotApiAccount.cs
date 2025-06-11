using CryptoExchange.Net.Objects;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Toobit.Net.Clients.SpotApi;
using Toobit.Net.Interfaces.Clients.SpotApi;
using Toobit.Net.Objects.Models;
using CryptoExchange.Net;
using System;
using Toobit.Net.Enums;
using Toobit.Net.Objects.Internal;

namespace Toobit.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class ToobitRestClientSpotApiAccount : IToobitRestClientSpotApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly ToobitRestClientSpotApi _baseClient;

        internal ToobitRestClientSpotApiAccount(ToobitRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Balances

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitUserBalance>> GetBalancesAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/account", ToobitExchange.RateLimiter.Toobit, 1, true);
            return await _baseClient.SendAsync<ToobitUserBalance>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Withdraw

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitWithdrawResult>> WithdrawAsync(string asset, string address, decimal quantity, string? network = null, string? tag = null, string? vaspCode = null, string? targetPersonFirstName = null, string? targetPersonLastName = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("coin", asset);
            parameters.Add("address", address);
            parameters.AddString("quantity", quantity);
            parameters.AddOptional("chainType", network);
            parameters.AddOptional("addressExt", tag);
            parameters.AddOptional("vaspCode", vaspCode);
            parameters.AddOptional("targetPersonFirstName", targetPersonFirstName);
            parameters.AddOptional("targetPersonLastName", targetPersonLastName);
            parameters.AddOptional("clientOrderId", clientOrderId ?? ExchangeHelpers.RandomString(24));
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/account/withdraw", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitWithdrawResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Withdrawals

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitWithdrawal[]>> GetWithdrawalsAsync(string? asset = null, string? fromId = null, long? withdrawOrderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("coin", asset);
            parameters.AddOptional("fromId", fromId);
            parameters.AddOptional("withdrawOrderId", withdrawOrderId);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/account/withdrawOrders", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitWithdrawal[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deposit Address

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitDepositAddress>> GetDepositAddressAsync(string asset, string network, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("coin", asset);
            parameters.Add("chainType", network);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/account/deposit/address", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitDepositAddress>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deposits

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitDeposit[]>> GetDepositsAsync(string? asset = null, string? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("coin", asset);
            parameters.AddOptional("fromId", fromId);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/account/depositOrders", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitDeposit[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Transfer

        /// <inheritdoc />
        public async Task<WebCallResult> TransferAsync(long fromUid, long toUid, AccountType fromAccountType, AccountType toAccountType, string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("fromUid", fromUid);
            parameters.Add("toUid", toUid);
            parameters.AddEnum("fromAccountType", fromAccountType);
            parameters.AddEnum("toAccountType", toAccountType);
            parameters.Add("asset", asset);
            parameters.Add("quantity", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/subAccount/transfer", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.Code != 200)
                return result.AsDatalessError(new ServerError(result.Data.Code, result.Data.Message));

            return result.AsDataless();
        }

        #endregion

        #region Get Transaction History

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitTransaction[]>> GetTransactionHistoryAsync(int? accountType = null, string? asset = null, int? flowType = null, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("accountType", accountType);
            parameters.AddOptional("coin", asset);
            parameters.AddOptional("flowType", flowType);
            parameters.AddOptional("fromId", fromId);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/account/balanceFlow", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitTransaction[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Sub Accounts

        /// <inheritdoc />
        public async Task<WebCallResult<ToobitSubAccount[]>> GetSubAccountsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/account/checkApiKey", ToobitExchange.RateLimiter.Toobit, 5, true);
            var result = await _baseClient.SendAsync<ToobitSubAccount[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Create a ListenKey 
        /// <inheritdoc />
        public async Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v1/userDataStream", ToobitExchange.RateLimiter.Toobit, 1, true);
            var result = await _baseClient.SendAsync<ToobitListenKey>(request, null, ct).ConfigureAwait(false);
            return result.As(result.Data?.ListenKey!);
        }

        #endregion

        #region Ping/Keep-alive a ListenKey

        /// <inheritdoc />
        public async Task<WebCallResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var parameters = new ParameterCollection
            {
                { "listenKey", listenKey }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Put, "api/v1/userDataStream", ToobitExchange.RateLimiter.Toobit, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Invalidate a ListenKey
        /// <inheritdoc />
        public async Task<WebCallResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var parameters = new ParameterCollection
            {
                { "listenKey", listenKey }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "api/v1/userDataStream", ToobitExchange.RateLimiter.Toobit, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
